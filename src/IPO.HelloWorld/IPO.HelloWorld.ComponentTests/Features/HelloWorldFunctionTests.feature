@function
Feature: Retrieve the HelloWorld greeting from the function app
  As an API consumer
  I want to retrieve the greeting
  So that I can confirm the function app returns its configured message

  Scenario: Return greeting successfully
    Given the function app is configured with the greeting "Hello Function Test!"
    When the HelloWorld function is invoked
    Then the function returns 200 OK with the greeting "Hello Function Test!"
