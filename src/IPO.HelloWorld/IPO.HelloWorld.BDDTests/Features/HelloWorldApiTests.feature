Feature: HelloWorldApiTests

The HelloWorld BDD tests

Scenario: Return greeting successfully
	Given There is a greeting in appConfig
	When apiURL HelloWorld requested
	Then the greeting is returned successfully

