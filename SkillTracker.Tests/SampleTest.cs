using NUnit.Framework;

namespace SkillTracker.Tests
{
    [TestFixture]
    public class SampleTest
    {
        [Test]
        [Category("SampleTest")]
        public void TestMethod_Success()
        {
            Assert.AreEqual(true, true);
        }

        [Test]
        [Category("SampleTest")]
        public void TestMethod_GetServerFileName_Success()
        {
            var file1 = "test.png";
            var file1_mod = Service.Controllers.Util.GetServerFileName(file1, "random123");
            Assert.AreEqual(file1_mod, "test_random123.png");

            var file2 = "test.12drh34.Sasa232.jpg";
            var file2_mod = Service.Controllers.Util.GetServerFileName(file2, "x1235s4d578s");
            Assert.AreEqual(file2_mod, "test.12drh34.Sasa232_x1235s4d578s.jpg");

            var file3 = "jpg.png";
            var file3_mod = Service.Controllers.Util.GetServerFileName(file3, "!@#$%");
            Assert.AreEqual(file3_mod, "jpg_!@#$%.png");

            var file4 = "1215...sads...png";
            var file4_mod = Service.Controllers.Util.GetServerFileName(file4, "12iuio12");
            Assert.AreEqual(file4_mod, "1215...sads.._12iuio12.png");
        }
    }
}
