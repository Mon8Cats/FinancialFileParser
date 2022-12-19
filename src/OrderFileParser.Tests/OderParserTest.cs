using OrderFileParser;

namespace OrderFileParser.Tests;


public class OrderParserTest
{
    [Fact]
    public void Should_Return_Order_When_ParseOrderHeader_With_CorrectInput()
    {
        var orderParser = new OrderParser();
        var input = "100    157685    5    167.9901/15/2021 15:34:17Joann Wilson                                      (210) 123-1234                jwilson@gmail.com                                 111";

        var result = orderParser.ParseOrderHeader(input);

        Assert.True(result.GetType() == typeof(OrderFileParser.Models.Order));

    }


    [Fact]
    public void Should_Return_Address_When_ParseAddress_With_CorrectInput()
    {
        var orderParser = new OrderParser();
        var input = "2001751 Maple Dr                                     Apt 114                                           Austin                                            TX78123";

        var result = orderParser.ParseAddress(input);

        Assert.True(result.GetType() == typeof(OrderFileParser.Models.Address));

    }

    [Fact]
    public void Should_Return_OrderDetail_When_ParseOrderDetail_With_CorrectInput()
    {
        var orderParser = new OrderParser();
        var input = "300 1    4     25.00    100.00Lawn Chair ";

        var result = orderParser.ParseOrderDetail(input);

        Assert.True(result.GetType() == typeof(OrderFileParser.Models.OrderDetail));

    }

    [Fact]
    public void Should_ThrowsException_When_ParseOrderDetail_With_InCorrectTotalPrice()
    {
        var orderParser = new OrderParser();
        var input = "300 1    4     25.00    100.01Lawn Chair ";

        Action action = () => orderParser.ParseOrderDetail(input);

        var caughtException = Assert.Throws<Exception>(action);
        Assert.Contains("Incorrect total price in Order Line", caughtException.Message);

    }

    [Fact]
    public void Should_ThrowsException_When_ParseOrderHeader_With_InCorrectDateTime()
    {
        var orderParser = new OrderParser();
        var input = "100    157693    1     15.1402/29/2021 12:17:19John David Smith                                  (210) 123-1235                jd_smith@gmail.com                                110";

        Action action = () => orderParser.ParseOrderHeader(input);

        var caughtException = Assert.Throws<System.FormatException>(action);
        Assert.True(caughtException.GetType() == typeof(System.FormatException));

    }

    [Fact]
    public void Should_ThrowsException_When_ParseOrderDetail_With_InCorrectPriceFormat()
    {
        var orderParser = new OrderParser();
        var input = "300 2    1     67.99     67.9APatio Table ";

        Action action = () => orderParser.ParseOrderDetail(input);

        var caughtException = Assert.Throws<System.FormatException>(action);
        Assert.True(caughtException.GetType() == typeof(System.FormatException));

    }
}
