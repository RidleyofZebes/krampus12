# Map Generator

For _12 Nights of Krampus_

To generate a map, run:

`MapGenerator.DrunkenBox(%Width%, %Height%, %FillTarget%)`

or

`MapGenerator.DrunkenWalk(%StepTarget%)`

Examples:

```csharp
var map = MapGenerator.DrunkenBox(1000, 1000, 50);

var map = MapGenerator.DrunkenWalk(1024);
```

More generation algorithims will be available in time.

## ToDo

- Structure generation
- Building layout generation
