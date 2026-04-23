# Geospatial Distance Analyzer (TacoParser): TDD-Driven C# Parser

This project is a high-performance C# utility designed to process large-scale geospatial datasets from CSV sources. Built using Test-Driven Development (TDD), the application parses Taco Bell location data to calculate the maximum geographical spread between any two points in the dataset. By utilizing System.IO and interface-based design, I transformed a simple data-parsing task into a scalable, production-ready utility that handles real-world 'dirty' data with grace.

+ Engineered a robust ingestion engine using System.IO that validates raw CSV files.
+ Architected the entire solution through a TDD lifecycle, ensuring 100% logic coverage and high confidence in edge-case handling.
+ Implemented decoupled components via Interface-based design, ensuring the parser is extensible for different data sources or provider types.
+ Carried out granular error logging for malformed data, null entries, parsing failures, and filesystem exceptions.
+ Leveraged the research of 'GeoCoordinatePortable' NuGet package to perform complex spherical distance calculations, and unit conversions from the output.
+ Conducted Code Reviews through peer and senior-level, focusing on DRY principles and performance optimization.

### Technologies: C#, CSV, LINQ, Git, Notepad++, Visual Studio
