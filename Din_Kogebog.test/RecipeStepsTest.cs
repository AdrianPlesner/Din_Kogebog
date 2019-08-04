using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Din_Kogebog.test
{
    [TestClass]
    public class RecipeStepsTest
    {
        [TestMethod]
        public void AddStepAdded()
        {
            var rec = new Recipe("Rec");
            string expected = "1. Udrør gær i mælken. \n";

            rec.AddStep(1, "Udrør gær i mælken.");
            string result = rec.PrintSteps();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EditStepChanges()
        {
            var rec = new Recipe("Rec");
            string expected = "1. Mælken lunes og gæren udrøres heri \n";
            rec.AddStep(1, "Udrør gær i mælken.");

            rec.EditStep(1, "Mælken lunes og gæren udrøres heri");
            string result = rec.PrintSteps();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EditStepNotSame()
        {
            var rec = new Recipe("Rec");
            string notExpected = "1. Udrør gær i mælken. \n";

            rec.AddStep(1, "Udrør gær i mælken.");
            rec.EditStep(1, "Mælken lunes");
            string result = rec.PrintSteps();

            Assert.AreNotEqual(notExpected, result);
        }

        [TestMethod]
        public void AddStepWithExistingNumber()
        {
            var rec = new Recipe("Rec");
            string expected = "1. Udrør gær i mælken. \n"
                + "2. Tilsæt æg. \n";

            rec.AddStep(1, "Tilsæt æg.");
            rec.AddStep(1, "Udrør gær i mælken.");
            string result = rec.PrintSteps();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RemoveStepMovingOthersInFront()
        {
            var rec = new Recipe("Rec");
            string expected = "1. Udrør gær i mælken. \n";
            rec.AddStep(1, "Lun mælken til den er håndvarm.");
            rec.AddStep(2, "Udrør gær i mælken.");

            rec.RemoveStep(1);
            string result = rec.PrintSteps();

            Assert.AreEqual(expected, result);
        }
    }
}
