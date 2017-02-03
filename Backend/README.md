## Instructions to add a new lambda function
**Say you want to create a new lambda function called TestService**

1. Create a directory under `src/` called `TestService`;
1. Copy `aws-lambda-tools-defaults.json`, `Function.cs` and `project.json` from `src/ExampleFunction` to `src/TestService`;
1. Modify `aws-lambda-tools-defaults.json`:
    - Change `function-name` to `TestService`,
    - Change `function-handler` to match current function pattern, usually change `ExampleFunction` to `TestService`.
1. Modify `Function.cs`:
    - Change namespace to `TestService`.
1. Modify `project.json`, add dependencies;
1. Run `dotnet restore` under `src/TestService` to see if there's any problem;
1. Start writing code ;)

## Instructions to add a new unit test
**Say you want to create a new unit test for TestService**

1. Create a directory under `test/` called `TestService.Test`;
1. Copy `FunctionTest.cs` and `project.json` from `test/ExampleFunction.Test` to `test/TestService.Test`;
1. Modify `FunctionTest.cs`:
    - Change using statement to `TestService`,
    - Change namespace to `TestService.Test`.
1. Modify `project.json`:
    - Change project name for testing under `dependencies` from `ExampleFunction` to `TestService`,
    - Add other dependencies.
1. Run `dotnet restore` under `src/TestService` to see if there's any problem;
1. Start writing test code ;)
