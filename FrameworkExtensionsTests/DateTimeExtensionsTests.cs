using FrameworkExtensions;

namespace FrameworkExtensionsTests;

[TestClass]
public class DateTimeExtensionsTests
{
    [TestMethod]
    public void YearsBeforeDate_InFuture_ReturnsZero()
    {
        DateTime firstOfYear2000 = new DateTime(2000, 1, 1);
        DateTime firstOfYear2026 = new DateTime(2026, 1, 1);
        int actual = firstOfYear2026.YearsBeforeDate(firstOfYear2000);
        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void YearsBeforeDate_InPast_ReturnsYears()
    {
        DateTime firstOfYear2000 = new DateTime(2000, 1, 1);
        DateTime firstOfYear2026 = new DateTime(2026, 1, 1);
        int actual = firstOfYear2000.YearsBeforeDate(firstOfYear2026);
        Assert.AreEqual(26, actual);
    }

    [TestMethod]
    public void YearsBeforeDate_SameYear_ReturnsZero()
    {
        DateTime firstOfYear2000 = new DateTime(2000, 1, 1);
        DateTime lastOfYear2000 = new DateTime(2000, 12, 31);
        int actual = firstOfYear2000.YearsBeforeDate(lastOfYear2000);
        Assert.AreEqual(0, actual);

    }

    [TestMethod]
    public void YearsBeforeDate_LeapYearBoundaryToEarlierDay_ReturnsYearsLessOne()
    {
        DateTime leapDate = new DateTime(2000, 2, 29);
        DateTime nonLeapDate = new DateTime(2023, 2, 28);
        int actual = leapDate.YearsBeforeDate(nonLeapDate);
        Assert.AreEqual(22, actual);
    }

    [TestMethod]
    public void YearsBeforeDate_LeapYearBoundaryToLaterDay_ReturnsYears()
    {
        DateTime leapDate = new DateTime(2000, 2, 29);
        DateTime nonLeapDate = new DateTime(2023, 3, 1);
        int actual = leapDate.YearsBeforeDate(nonLeapDate);
        Assert.AreEqual(23, actual);
    }

    [TestMethod]
    public void YearsBeforeToday_FutureDate_ReturnsZero()
    {
        DateTime fiveDaysInFuture = DateTime.Today.AddDays(5);
        int actual = fiveDaysInFuture.YearsBeforeToday();
        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void YearsBeforeToday_ThreeHundredSixtyFourDaysInPast_ReturnsZero()
    {
        DateTime almostYearPast = DateTime.Today.AddDays(-364);
        int actual = almostYearPast.YearsBeforeToday();
        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void YearsBeforeToday_ThreeHundredSixtySevenDaysInPast_ReturnsOne()
    {
        DateTime justOverYearPast = DateTime.Today.AddDays(-367);
        int actual = justOverYearPast.YearsBeforeToday();
        Assert.AreEqual(1, actual);

    }

}