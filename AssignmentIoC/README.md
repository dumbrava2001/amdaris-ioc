## What code smells did you see?

1. Nested classes inside for defining custom exceptions. (Needless Complexity)
2. Commented code that is not used and also could lead to bugs if uncommented because it could be outdated
3. Rigidity by having a nested if else checks
4. Needless Repetition by applying default values for primitive locals ones.
5. Opacity for ambiguous variables names
6. Magic numbers

## What problems do you think the Speaker class has?

1. It does multiple things not related to its purpose (having nesting classes)
2. Commented code
3. Invalid ArgumentNullException class provided parameters

## Which clean code principles (or general programming principles) did it violate?

1. SRP (nesting classes)
2. OCP (hardcoded lists)
3. KISS

## What refactoring techniques did you use?

1. Extracting behaviour and reduce complexity by creating individual separated classes