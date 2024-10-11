namespace Web.Utilities
{
    public static class ArticleGenerator
    {
        public static string GenerateArticle(string categoryName)
        {
            // Уберем все пробелы и возьмем первые 3 символа названия категории (для префикса)
            string prefix = new string(categoryName.Where(char.IsLetter).ToArray()).ToUpper().Substring(0, 3);

            // Возьмем уникальную часть из GUID
            string uniquePart = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

            // Добавим текущий год
            string year = DateTime.UtcNow.Year.ToString();

            // Сгенерируем артикул в формате PREFIX-UNIQUE-YEAR
            return $"{prefix}-{uniquePart}-{year}";
        }
    }
}
