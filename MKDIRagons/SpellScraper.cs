using HtmlAgilityPack;

namespace MKDIRagons
{
    public class SpellScraper
    {
        public async Task<Spell> ScrapeSpellAsync(string spell)
        {
            string spellName = spell.Replace(" ", "-").ToLower();
            using HttpClient client = new HttpClient();

            string html = await client.GetStringAsync($"https://dnd5e.wikidot.com/spell:{spellName}");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            Spell spellObj = new Spell();
            var titleNode = doc.DocumentNode.SelectSingleNode("//div[@class='page-title page-header']/span");
            if (titleNode != null)
                spellObj.Name = titleNode.InnerText.Trim();

            var contentDiv = doc.DocumentNode.SelectSingleNode("//div[@id='page-content']");
            var paragraphs = contentDiv?.SelectNodes(".//p");
            if (paragraphs != null)
            {
                foreach (var p in paragraphs)
                    spellObj.AddSection(p.InnerText.Trim());
            }

            return spellObj;
        }
    }
}
