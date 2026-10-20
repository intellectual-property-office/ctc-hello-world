@api
Feature: Retrieve the HelloWorld greeting from the API
  As an API consumer
  I want to retrieve the greeting
  So that I can confirm the service returns its configured message

  Scenario: Return greeting successfully
    Given the service is configured with the greeting "Hello Component Test!"
    When I send a GET request to "/"
    Then the response status is 200 OK
    And the response body contains the greeting "Hello Component Test!"

