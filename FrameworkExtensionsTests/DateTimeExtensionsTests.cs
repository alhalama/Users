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

}