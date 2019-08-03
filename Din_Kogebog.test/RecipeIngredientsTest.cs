using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Din_Kogebog.test
{
    [TestClass]
    public class RecipeIngredientsTest
    {
        [TestMethod]
        public void AddIngredientAdded()
        {
            var rec = new Recipe("Rec");
            string expected = "100 g mel\n".Trim();

            rec.AddIngredient("mel", (100, Unit.g));
            string result = rec.PrintIngredientList().Trim();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EditIngredientChanges()
        {
            var rec = new Recipe("Rec");
            bool result = false;

            rec.AddIngredient("mel", (100, Unit.g));
            result = rec.PrintIngredientList().Trim() == "100 g mel";
            rec.EditIngredient("mel", (0.10, Unit.kg));
            result &= rec.PrintIngredientList().Trim() == "0.1 kg mel";

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EditIngredientNotSame()
        {
            var rec = new Recipe("Rec");
            string notExpected = "100 g mel\n".Trim();

            rec.AddIngredient("mel", (100, Unit.g));
            rec.EditIngredient("mel", (0.1, Unit.kg));
            string result = rec.PrintIngredientList().Trim();

            Assert.AreNotEqual(notExpected, result);
        }
        
    }
}
