# C# Libraries Documentation Index

## Overview
This workspace contains two utility libraries for C#/.NET 8.0 projects that provide functionality for date/time operations and timer management.

---

## Libraries

### 1. **DateFromTimeSpan Library**
A comprehensive utility library for date and time calculations, including conversions between different time units and date validation.

- **Namespace**: `DateFromTimeSpan`
- **.NET Framework**: net8.0
- **Type**: Static utility classes
- **Documentation**: [DateFromTimeSpan_DOCUMENTATION.md](DateFromTimeSpan_DOCUMENTATION.md)
- **Key Features**:
  - Convert days to hours, minutes, and seconds
  - Calculate months and years from days
  - Validate dates and birth dates
  - Calculate time differences between dates
  - Check string date
  - Extract dates from DateTimePicker

---

### 2. **LibTimer Library**
A lightweight timer library providing core timer functionality with hour, minute, and second management.

- **Namespace**: `LibTimer`
- **.NET Framework**: net8.0
- **Type**: Sealed class with interface implementation
- **Documentation**: [LibTimer_DOCUMENTATION.md](LibTimer_DOCUMENTATION.md)
- **Key Features**:
  - Time component management (hours, minutes, seconds)
  - Individual update methods for each time component
  - Reset functionality for all time components
  - Property-based access to time values

---

## Quick Navigation

| Library | Files | Main Class | Purpose |
|---------|-------|-----------|---------|
| DateFromTimeSpan | clsTimeSpanUtils.cs, clsTimeSpanUtils2.cs | clsTimeSpanUtils | Date/Time calculations and conversions |
| LibTimer | clsTimerCore.cs, Icore.cs | clsTimerCore | Timer component management |

---

## How to Use These Libraries

1. Reference the respective .csproj files in your project
2. Include the namespace: `using DateFromTimeSpan;` or `using LibTimer;`
3. Access static methods (DateFromTimeSpan) or create instances (LibTimer)
4. See individual documentation files for detailed API references

---

**Last Updated**: July 2026
