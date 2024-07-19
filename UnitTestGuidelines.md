# Unit Test Guidelines

## Rules

- The latest version of NUnit testing framework should be used for generating unit test code.
- The AAA (Arrange-Act-Assert) pattern should be used for organizing test methods.
- For asserting outcomes, `Assert.That` should be used for its flexibility and readability.
- When necessary to isolate some slow or difficult to setup dependency, latest version of Moq mocking framework should be used.
