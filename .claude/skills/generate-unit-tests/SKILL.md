---
name: generate-unit-tests
description: Generate NUnit unit tests for a type in AdaptiveNetwork.Geometry.Core, following this repo's existing test conventions. Use when asked to write, add, or generate unit tests for a class/struct in this solution.
---

# Generate unit tests

Write tests matching the conventions already established in `AdaptiveNetwork.Geometry.Core.Tests` (see `Types/Point3D_Tests` for the reference example). Don't invent a new style.

## File layout

For a type `Foo`, create/use:
- `Types/Foo_Tests/Tests.cs` — the `[TestFixture] internal sealed class Tests`
- `Types/Foo_Tests/TestCaseData.cs` — only if you need parameterized cases

Namespace: `AdaptiveNetwork.Geometry.Core.Tests.Types.Foo_Tests`.

## Conventions

- NUnit 4, `[TestFixture]` / `[Test]`, class is `internal sealed class Tests`.
- Test method names: `MethodName_ShouldExpectedBehavior` or `MethodName_ShouldExpectedBehavior_WhenCondition`.
- One behavior per test. Use `using (Assert.EnterMultipleScope())` (or `Assert.Multiple(() => ...)`) when asserting more than one thing.
- For methods taking several input combinations, add a `TestCaseSource` pointing at a nested class in `TestCaseData.cs`:
  - A plain data record (`internal sealed class XData { public ... { get; init; } }`)
  - A provider class (`internal sealed class XTestCaseData : NUnit.Framework.TestCaseData` for grouping, actual data via `public static IEnumerable<XData> Data { get { yield return ...; } }`)
  - Cover: zero/identity case, positive, negative, and at least one boundary case.
- Types implementing `IEquatableWithinTolerance<T>` need tests for: equal within tolerance, not equal outside tolerance, exact match with default (zero) tolerance, mismatch with default tolerance.
- Types implementing `ISelfTranslatable`/`ISelfTransformable` need one test per translate/transform overload, asserting all affected components.
- If `GetHashCode`/`Equals(object)` are unimplemented (`throw new NotImplementedException()` — check the source first), assert they throw; don't assume they're implemented.
- Bogus is available (`PackageReference Bogus`) for randomized inputs if a fixed literal case isn't meaningful — prefer literals for anything asserting exact values.

## Workflow

1. Read the target type's source file fully (properties, methods, interfaces implemented) — don't guess its API.
2. Check if `Types/<Type>_Tests/` already exists; extend it rather than duplicating.
3. Write tests, then run them:
   ```
   dotnet test AdaptiveNetwork.Geometry.Core.Tests
   ```
