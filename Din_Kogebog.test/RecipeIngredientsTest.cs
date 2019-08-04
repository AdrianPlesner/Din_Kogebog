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
            string expected = "100 g mel \n";

            rec.AddIngredient("mel", (100, Unit.g));
            string result = rec.PrintIngredientList();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EditIngredientChanges()
        {
            var rec = new Recipe("Rec");
            bool result = false;

            rec.AddIngredient("mel", (100, Unit.g));
            result = rec.PrintIngredientList() == "100 g mel \n";
            rec.EditIngredient("mel", (0.10, Unit.kg));
            result &= rec.PrintIngredientList() == "0.1 kg mel \n";

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EditIngredientNotSame()
        {
            var rec = new Recipe("Rec");
            string notExpected = "100 g mel \n";

            rec.AddIngredient("mel", (100, Unit.g));
            rec.EditIngredient("mel", (0.1, Unit.kg));
            string result = rec.PrintIngredientList();

            Assert.AreNotEqual(notExpected, result);
        }

        [TestMethod]
        public void RemoveIngredient()
        {
            var rec = new Recipe("Rec");
            bool result = false;

            rec.AddIngredient("mel", (100, Unit.g));
            rec.AddIngredient("mælk", (1, Unit.dl));
            result = rec.PrintIngredientList() == "100 g mel \n1 dl mælk \n";
            rec.RemoveIngredient("mælk");
            result &= rec.PrintIngredientList() == "100 g mel \n";
            

            Assert.IsTrue(result);
        }
        
    }
}
