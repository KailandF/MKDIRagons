using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace MKDIRagons
{
    internal class SpellScraper
    {
        public async Task<Spell> ScrapeSpellAsync(string spell)
        {
            // Convert the user inputed spell into a URL-friendly form.
            string spellName = spell.Replace(" ", "-").ToLower();

            using HttpClient client = new HttpClient();

            // Access wikidot using the spellname
            string html = await client.GetStringAsync($"https://dnd5e.wikidot.com/spell:{spellName}");

            // Open web page
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Create and populate spell object
            return PopulateSpellFromDocument(doc);
        }

        private Spell PopulateSpellFromDocument(HtmlDocument doc)
        {
            Spell spellObj = new Spell();

            // Set spell name
            var titleNode = doc.DocumentNode.SelectSingleNode("//div[@class='page-title page-header']/span");
            if (titleNode != null)
                spellObj.Name = titleNode.InnerText.Trim();

            // Set spell source (sourcebooks)
            var sourceNode = doc.DocumentNode.SelectSingleNode("//p[contains(text(), 'Source:')]");
            if (sourceNode != null)
            {
                spellObj.Source = sourceNode.InnerText.Trim();
            }

            // Extract spell level and school
            SetLevelAndSchool(doc, spellObj);
            SetCastingTime(doc, spellObj);
            SetRange(doc, spellObj);
            SetComponents(doc, spellObj);
            SetDuration(doc, spellObj);
            SetDescription(doc, spellObj);
            SetUpcast(doc, spellObj);

            return spellObj;
        }

        private void SetLevelAndSchool(HtmlDocument doc, Spell spellObj)
        {
            var levelSchoolNode = doc.DocumentNode.SelectSingleNode("//em[contains(text(), '-level') or contains(text(), ' Cantrip')]");
            if (levelSchoolNode != null)
            {
                string levelSchoolText = levelSchoolNode.InnerText.Trim();

                // Handle X-level format
                var levelMatch = Regex.Match(levelSchoolText, @"(\d+)(?:st|nd|rd|th)-level (\w+)");
                if (levelMatch.Success)
                {
                    spellObj.Level = levelMatch.Groups[1].Value + "-level";
                    spellObj.School = levelMatch.Groups[2].Value;
                }
                // Handle Cantrip format
                else
                {
                    var cantripMatch = Regex.Match(levelSchoolText, @"(\w+) Cantrip");
                    if (cantripMatch.Success)
                    {
                        spellObj.Level = "Cantrip";
                        spellObj.School = cantripMatch.Groups[1].Value;
                    }
                }
            }
        }

        private void SetCastingTime(HtmlDocument doc, Spell spellObj)
        {
            var castingTimeNode = doc.DocumentNode.SelectSingleNode("//strong[contains(text(), 'Casting Time:')]/following-sibling::text()[1]");
            if (castingTimeNode != null)
            {
                spellObj.CastingTime = castingTimeNode.InnerText.Trim();
            }
        }

        private void SetRange(HtmlDocument doc, Spell spellObj)
        {
            var rangeNode = doc.DocumentNode.SelectSingleNode("//strong[contains(text(), 'Range:')]/following-sibling::text()[1]");
            if (rangeNode != null)
            {
                spellObj.Range = rangeNode.InnerText.Trim();
            }
        }

        private void SetComponents(HtmlDocument doc, Spell spellObj)
        {
            var componentsNode = doc.DocumentNode.SelectSingleNode("//strong[contains(text(), 'Components:')]/following-sibling::text()[1]");
            if (componentsNode != null)
            {
                spellObj.Components = componentsNode.InnerText.Trim();
            }
        }

        private void SetDuration(HtmlDocument doc, Spell spellObj)
        {
            var durationNode = doc.DocumentNode.SelectSingleNode("//strong[contains(text(), 'Duration:')]/following-sibling::text()[1]");
            if (durationNode != null)
            {
                spellObj.Duration = durationNode.InnerText.Trim();
            }
        }

        private void SetDescription(HtmlDocument doc, Spell spellObj)
        {
            var descriptionNode = doc.DocumentNode.SelectSingleNode("//strong[contains(text(), 'Duration:')]/parent::p/following-sibling::p");
            if (descriptionNode != null)
            {
                spellObj.Description = descriptionNode.InnerText.Trim();
            }
        }

        private void SetUpcast(HtmlDocument doc, Spell spellObj)
        {
            var upcastNode = doc.DocumentNode.SelectSingleNode("//p[contains(text(), 'At Higher Levels.')]");
            if (upcastNode != null)
            {
                // Extract text after "At Higher Levels."
                var match = Regex.Match(upcastNode.InnerText, @"At Higher Levels\.\s*(.+)$", RegexOptions.Singleline);
                if (match.Success)
                {
                    spellObj.Upcast = match.Groups[1].Value.Trim();
                }
            }
            else // If spell has no upcast feature
            {
                spellObj.Upcast = "N/A";
            }
        }
    }
}