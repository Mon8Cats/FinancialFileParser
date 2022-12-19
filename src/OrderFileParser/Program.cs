using  OrderFileParser;

internal class Program
{
    private static void Main(string[] args)
    {
        //var fileName = "OrderFile.txt";
        var fileName = "OrderFileCorrect.txt";
        //var fileName = "OrderFileIncorrectDateTime.txt";
        //var fileName = "OrderFileIncorrectPriceFormat.txt";
        //var fileName = "OrderFileIncorrectOrderHeader.txt";
        //var fileName = "OrderFileIncorrectOrderDetail.txt";
        //var fileName = "OrderFileIncorrectTypeIndicator.txt";

        var inputPath = $"InputFiles/{fileName}";
        var outputPath = $"OutputFiles/{fileName}";
        var orderParser = new OrderParser();

        var orders = orderParser.ParseOrders(inputPath);
        orderParser.WriteToFile(outputPath);
    }
}