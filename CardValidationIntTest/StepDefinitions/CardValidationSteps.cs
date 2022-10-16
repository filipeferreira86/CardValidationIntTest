using CardValidationIntTest.Support;
using NUnit.Framework;

namespace CardValidationIntTest.StepDefinitions
{
    [Binding]
    internal class CardValidationSteps
    {
        private SupportData data;
        [Given(@"That I have the API adress")]
        public void GivenThatIHaveTheAPIAdress()
        {
            data = new SupportData();
        }

        [When(@"I send the right data for (.*)")]
        public void WhenISendTheRightDataFor(string cardType)
        {
            if (!data.SendCardData(cardType))
            {
                Assert.True(false, message: data.error);
            }
        }

        [When(@"I send the card data missing the (.*)")]
        public void WhenISendTheCardDataMissingTheBlankField(string missingField)
        {
            data.SendCardData(missingField, null);
            if (data.error.ToLower() != "badrequest")
            {
                Assert.True(false, message: "As the field " + missingField + " is blank the return should be bad request\n" +
                    "but the return was " + data.error);
            }
        }

        [When(@"I send the card data with wrong (.*)")]
        public void WhenISendTheCardDataWithWrong(string wrongField)
        {
            data.SendCardData(null, wrongField);
            if (data.error.ToLower() != "badrequest")
            {
                Assert.True(false, message: "As the field " + data.wrongFieldName + " is wrong the return should be bad request\n" +
                    "but the return was " + data.error);
            }
        }



        [Then(@"the return should be (.*)")]
        public void ThenTheReturnShouldBe(string cardType)
        {
            string cardTypeReturned = data.GetCardTypeReturn();
            switch (cardType.ToLower())
            {
                case "mastercard":
                    if (cardTypeReturned == "20")
                    {
                        Assert.True(true);
                    }
                    else
                    {
                        Assert.False(true, message: "Card type should be 20 (mastercard),\n but is " + cardTypeReturned);
                    }
                    break;
                case "visa":
                    if (cardTypeReturned == "10")
                    {
                        Assert.True(true);
                    }
                    else
                    {
                        Assert.False(true, message: "Card type should be 10 (Visa),\n but is " + cardTypeReturned);
                    }
                    break;
                case "american axpress":
                    if (cardTypeReturned == "30")
                    {
                        Assert.True(true);
                    }
                    else
                    {
                        Assert.False(true, message: "Card type should be 30 (American Express),\n " +
                            "but is " + cardTypeReturned);
                    }
                    break;
            }
        }

        [Then(@"the return error message should be wrong (.*)")]
        public void ThenTheReturnErrorMessageShouldBeWrongWrongField(string wrongField)
        {
            string errorMessage = "{\"" + data.wrongFieldName + "\":[\"Wrong " + data.wrongFieldName + "\"]}";
            Assert.AreEqual(errorMessage.ToLower(), data.errorContent.ToLower());
        }



        [Then(@"the return error message should be (.*) is required")]
        public void ThenTheReturnErrorMessageShouldBeOwnerIsRequired(string missingField)
        {
            string errorMessage = "{\"" + missingField + "\":[\"" + missingField + " is required\"]}";
            Assert.AreEqual(errorMessage.ToLower(), data.errorContent.ToLower());
        }

    }
}
