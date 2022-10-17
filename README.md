# CardValidationIntTest

# card-validation-master


Hello!

Description of the task: This is a Readme file for the Arvato QA engineer interview. As part of the assignment I should create an unit test framework and an integration test framework.
Here you can find the automation for the integration test, you can also find the integration test in the link below:
https://github.com/filipeferreira86/card-validation-master

## Integration test
In this repository you will find the Integration test framework built with NUnit, RestSharp and Specflow. The approach for this test was to test not only each field but the return of the API for success situations for each type of card requested in the test exercise file and the error situations for the most important situations. To do so, I'm considering the API running on the local machine at port 7135, it can be changed just changing the address for it in the field "url" in the cardData.json inside the Data folder of the project.

## Running the tests

To run this test you must have the API running in the address shown in the cardData.json, for this you need to have the project that you can find in this address: https://github.com/filipeferreira86/card-validation-master
1) You will need to have an IDE to build this test, you can use the Visual Studio.
2) Clone this project to your local repository
3) Open the IDE and build the project
4) Running the tests
  4.1) If you're using Visual Studio you can use the test explorer, just clicking on the "Run all button"
  4.2) If you're not using the Visual studio or want to run it on command line or even want to run it on a pipeline, you can run it with the instructipon below in a command line:
    ```dotnet test --no-build```
