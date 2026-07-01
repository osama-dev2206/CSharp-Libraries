# DateFromTimeSpan Library Documentation

## Table of Contents

1. [Overview](#overview)
2. [Architecture](#architecture)
3. [Classes](#classes)
4. [API Reference](#api-reference)
5. [Usage Examples](#usage-examples)
6. [Implementation Details](#implementation-details)

---

## Overview

The **DateFromTimeSpan** library is a utility library designed for date and time operations in C#. It provides comprehensive methods for converting between different time units (days, months, years, hours, minutes, seconds) and validating dates.

- **Target Framework**: .NET 8.0
- **Namespace**: `DateFromTimeSpan`
- **Type**: Static utility class with nested partial class
- **Accessibility**: Public API for all utility methods

### Key Capabilities

- Time unit conversions (days ↔ hours/minutes/seconds)
- Month and year calculations from days
- Date validation and leap year handling
- Birth date validation
- Time difference calculations between two dates
- DateTimePicker integration

---

## Architecture

The library consists of two classes:

### Class Structure

```
DateFromTimeSpan
├── clsTimeSpanUtils (Primary class)
├── clsTimeSpanUtils2 (DateTime_Calculator namespace - extension)
└── Helper methods (Private)
```

### Class Relationships

- **clsTimeSpanUtils**: Main public API
- **clsTimeSpanUtils2**: Partial class in `DateTime_Calculator` namespace providing additional date comparison functionality
- Both are **static classes** - no instantiation needed

---

## Classes

### clsTimeSpanUtils (Primary Class)

**Namespace**: `DateFromTimeSpan`  
**Access**: `public static class`  
**Purpose**: Contains all time conversion and month/year calculation utilities

**Methods Overview**:

- Time conversion methods
- Month/year calculation methods
- Helper validation methods

---

### clsTimeSpanUtils2 (DateTime_Calculator)

**Namespace**: `DateFromTimeSpan.DateTime_Calculator`  
**Access**: `public static partial class`  
**Purpose**: DateTime validation and comparison utilities

**Methods Overview**:

- Date string parsing and validation
- Birth date validation
- Date difference calculations

---

## API Reference

### Time Conversion Methods

#### 1. GetNumOfDaysInMonth

```csharp
public static byte GetNumOfDaysInMonth(byte month, int year)
```

**Description**: Returns the number of days in a specific month, accounting for leap years.

**Parameters**:

- `byte month`: Month number (1-12)
- `int year`: Year (handles leap year calculation)

**Returns**: `byte` - Number of days in the month (0 if invalid month)

**Features**:

- Automatically detects leap years for February
- Validates month range (1-12)
- Returns 28 or 29 for February based on leap year
- Returns standard days for other months (30 or 31)

**Example**:

```csharp
byte days = clsTimeSpanUtils.GetNumOfDaysInMonth(2, 2024);  // Returns 29 (leap year)
byte days = clsTimeSpanUtils.GetNumOfDaysInMonth(2, 2023);  // Returns 28 (non-leap)
```

---

#### 2. GetHoursFromDays

```csharp
public static int GetHoursFromDays(int days)
```

**Description**: Converts days to hours.

**Parameters**:

- `int days`: Number of days

**Returns**: `int` - Equivalent hours (days × 24)

**Example**:

```csharp
int hours = clsTimeSpanUtils.GetHoursFromDays(5);  // Returns 120 hours
```

---

#### 3. GetMinsFromDays

```csharp
public static int GetMinsFromDays(int days)
```

**Description**: Converts days to minutes.

**Parameters**:

- `int days`: Number of days

**Returns**: `int` - Equivalent minutes (days × 24 × 60)

**Example**:

```csharp
int minutes = clsTimeSpanUtils.GetMinsFromDays(2);  // Returns 2880 minutes
```

---

#### 4. GetSecsFromDays

```csharp
public static int GetSecsFromDays(int days)
```

**Description**: Converts days to seconds.

**Parameters**:

- `int days`: Number of days

**Returns**: `int` - Equivalent seconds (days × 24 × 60 × 60)

**Example**:

```csharp
int seconds = clsTimeSpanUtils.GetSecsFromDays(1);  // Returns 86400 seconds
```

---

### Month/Year Calculation Methods

#### 5. GetNumOfMonthsFromYears

```csharp
public static int GetNumOfMonthsFromYears(int years)
```

**Description**: Converts years to months.

**Parameters**:

- `int years`: Number of years

**Returns**: `int` - Equivalent months (years × 12)

**Example**:

```csharp
int months = clsTimeSpanUtils.GetNumOfMonthsFromYears(2);  // Returns 24 months
```

---

#### 6. GetNumOfYearsFromDays (Overload 1)

```csharp
public static int GetNumOfYearsFromDays(int days)
```

**Description**: Converts days to years using a simple 365-day approximation.

**Parameters**:

- `int days`: Number of days

**Returns**: `int` - Approximate years (days ÷ 365)

**Example**:

```csharp
int years = clsTimeSpanUtils.GetNumOfYearsFromDays(730);  // Returns 2 years
```

---

#### 7. GetNumOfYearsFromDays (Overload 2)

```csharp
public static int GetNumOfYearsFromDays(int days, DateTime StartYear)
```

**Description**: Converts days to years using a start date for more accurate calculation.

**Parameters**:

- `int days`: Number of days
- `DateTime StartYear`: Reference start date for accurate calculation

**Returns**: `int` - Years calculated from the given start date

**Example**:

```csharp
DateTime start = new DateTime(2020, 1, 1);
int years = clsTimeSpanUtils.GetNumOfYearsFromDays(365, start);
```

---

#### 8. GetNumOfMonthsFromDays (Overload 1 - Simple)

**Description**: Simple calculation using a fixed approximation.

**Implementation**: Uses the overload with DateTime parameter internally.

---

#### 8. GetNumOfMonthsFromDays (Overload 2 - Accurate)

```csharp
public static int GetNumOfMonthsFromDays(int days, DateTime StartYear)
```

**Description**: Calculates months from days with high accuracy by accounting for varying month lengths.

**Parameters**:

- `int days`: Number of days
- `DateTime StartYear`: Starting date for calculation

**Returns**: `int` - Number of complete months

**Algorithm**:

1. Iterates through each month starting from the given date
2. Subtracts the number of days in each month
3. Increments month counter until days are exhausted
4. Handles month overflow (Dec → Jan) automatically

**Features**:

- Accounts for leap years
- Handles varying month lengths (28-31 days)
- More accurate than simple 30-day approximation

**Example**:

```csharp
DateTime start = new DateTime(2024, 1, 1);
int months = clsTimeSpanUtils.GetNumOfMonthsFromDays(90, start);
// Accurately calculates based on Jan (31) + Feb (29-leap) + Mar (30)
```

---

### DateTime Validation Methods (clsTimeSpanUtils2)

#### 9. CheckDate

```csharp
static public bool CheckDate(string Date, out DateTime Res)
```

**Description**: Validates if a string represents a valid date format and parses it.

**Parameters**:

- `string Date`: Date string to validate
- `out DateTime Res`: Output parameter receiving parsed DateTime if valid

**Returns**: `bool` - True if valid date format, false otherwise

**Example**:

```csharp
bool isValid = clsTimeSpanUtils.CheckDate("2024-07-01", out DateTime result);
if (isValid)
{
    Console.WriteLine($"Valid date: {result}");
}
```

---

#### 10. IsValidBirthDate

```csharp
static public bool IsValidBirthDate(DateTime DateOfBirth)
```

**Description**: Validates if a birth date is legitimate (in the past or today).

**Parameters**:

- `DateTime DateOfBirth`: Birth date to validate

**Returns**: `bool` - True if valid birth date (≤ current date), false otherwise

**Validation Rules**:

- Birth date must not be in the future
- Birth date must not be the default DateTime value
- Must be before or equal to current date/time

**Example**:

```csharp
DateTime birthDate = new DateTime(2000, 5, 15);
if (clsTimeSpanUtils.IsValidBirthDate(birthDate))
{
    Console.WriteLine("Valid birth date");
}
```

---

#### 11. GetDiffBetweenNowDateAndCurrentDate

```csharp
static public TimeSpan GetDiffBetweenNowDateAndCurrentDate(DateTime Date)
```

**Description**: Calculates the TimeSpan between the current date/time and a given date.

**Parameters**:

- `DateTime Date`: Date to compare with current date/time

**Returns**: `TimeSpan` - Time difference (current date - given date), or zero TimeSpan if invalid

**Validation**:

- Validates birth date using `IsValidBirthDate()`
- Returns zero TimeSpan for invalid dates

**Example**:

```csharp
DateTime birthDate = new DateTime(2000, 1, 1);
TimeSpan age = clsTimeSpanUtils.GetDiffBetweenNowDateAndCurrentDate(birthDate);
Console.WriteLine($"Age: {age.Days} days");
```

---

#### 12. GetDiffBetweenDate1AndDate2

```csharp
static public TimeSpan GetDiffBetweenDate1AndDate2(DateTime Date1, DateTime Date2)
```

**Description**: Calculates the TimeSpan between two dates.

**Parameters**:

- `DateTime Date1`: First date
- `DateTime Date2`: Second date

**Returns**: `TimeSpan` - Difference (Date1 - Date2), or zero TimeSpan if either date is invalid

**Validation**:

- Validates both dates using `CheckDate()`
- Converts default DateTime to current date automatically
- Returns zero TimeSpan for invalid inputs

**Example**:

```csharp
DateTime date1 = new DateTime(2024, 12, 31);
DateTime date2 = new DateTime(2024, 1, 1);
TimeSpan diff = clsTimeSpanUtils.GetDiffBetweenDate1AndDate2(date1, date2);
Console.WriteLine($"Days between: {diff.Days}");  // 364 days
```

---

#### 13. ExtractDateFromDateTimePicker

```csharp
static public string ExtractDateFromDateTimePicker(dynamic dateTimePicker)
```

**Description**: Extracts the date portion from a DateTimePicker control value.

**Parameters**:

- `dynamic dateTimePicker`: DateTimePicker control (WinForms)

**Returns**: `string` - Date portion without time component

**Implementation**:

- Gets string representation of picker value
- Removes everything after the space (time portion)
- Trims whitespace

**Example**:

```csharp
// For DateTimePicker showing "7/1/2024 2:30:45 PM"
string dateOnly = clsTimeSpanUtils.ExtractDateFromDateTimePicker(dateTimePicker1);
// Returns: "7/1/2024"
```

---

## Usage Examples

### Example 1: Calculate Age in Years

```csharp
using DateFromTimeSpan;

DateTime birthDate = new DateTime(1995, 3, 15);

// Validate birth date
if (clsTimeSpanUtils.IsValidBirthDate(birthDate))
{
    // Get time span from birth to now
    TimeSpan age = clsTimeSpanUtils.GetDiffBetweenNowDateAndCurrentDate(birthDate);
    int years = clsTimeSpanUtils.GetNumOfYearsFromDays(age.Days, birthDate);
    Console.WriteLine($"Age: {years} years");
}
```

---

### Example 2: Calculate Days Between Two Dates

```csharp
using DateFromTimeSpan;

DateTime startDate = new DateTime(2024, 1, 1);
DateTime endDate = new DateTime(2024, 12, 31);

TimeSpan difference = clsTimeSpanUtils.GetDiffBetweenDate1AndDate2(endDate, startDate);
Console.WriteLine($"Days: {difference.Days}");  // 364 days

// Convert to other units
int hours = clsTimeSpanUtils.GetHoursFromDays(difference.Days);
int minutes = clsTimeSpanUtils.GetMinsFromDays(difference.Days);
```

---

### Example 3: Calculate Months with Accuracy

```csharp
using DateFromTimeSpan;

DateTime start = new DateTime(2024, 1, 15);
int days = 100;

// Accurate month calculation accounting for varying month lengths
int months = clsTimeSpanUtils.GetNumOfMonthsFromDays(days, start);
Console.WriteLine($"100 days from Jan 15 = approximately {months} months");
```

---

### Example 4: Validate Date Input

```csharp
using DateFromTimeSpan;

string userInput = "2024-07-01";

if (clsTimeSpanUtils.CheckDate(userInput, out DateTime parsedDate))
{
    if (clsTimeSpanUtils.IsValidBirthDate(parsedDate))
    {
        Console.WriteLine("Valid birth date entered");
    }
    else
    {
        Console.WriteLine("Date must be in the past");
    }
}
else
{
    Console.WriteLine("Invalid date format");
}
```

---

### Example 5: Days in Month (Leap Year Handling)

```csharp
using DateFromTimeSpan;

// February in leap year
byte days2024 = clsTimeSpanUtils.GetNumOfDaysInMonth(2, 2024);  // 29 days

// February in non-leap year
byte days2023 = clsTimeSpanUtils.GetNumOfDaysInMonth(2, 2023);  // 28 days

// Other months
byte daysJan = clsTimeSpanUtils.GetNumOfDaysInMonth(1, 2024);   // 31 days
byte daysApr = clsTimeSpanUtils.GetNumOfDaysInMonth(4, 2024);   // 30 days
```

---

## Implementation Details

### Private Helper Methods

#### NumberOfDaysInMonth (Private)

```csharp
private static byte NumberOfDaysInMonth(byte m)
```

- Maintains an array of days per month: `{ 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }`
- Returns day count for months 1-12 (excludes February which is handled separately)
- Index 0 is 0 (unused)

#### IsValidMonth (Private)

```csharp
private static bool IsValidMonth(int month)
```

- Validates month is in range 1-12
- Uses absolute value to handle negative months
- Returns boolean indicating validity

#### AddMonthToDate (Private)

```csharp
private static void AddMonthToDate(ref DateTime StartYear)
```

- Increments DateTime by one month
- Uses `AddMonths(1)` internally
- Passed by reference to modify original DateTime

#### CheckDefaultDate (Private)

```csharp
private static void CheckDefaultDate(ref DateTime Date)
```

- Checks if DateTime is default value (0001-01-01)
- Replaces with current date/time if default
- Used to normalize empty DateTime parameters

---

### Leap Year Handling

The library correctly handles leap years using `DateTime.IsLeapYear()`:

- **Leap years**: Every 4 years
- **Exception**: Years divisible by 100 are NOT leap years
- **Exception to exception**: Years divisible by 400 ARE leap years

Example:

- 2024: Leap year (divisible by 4, not by 100)
- 2000: Leap year (divisible by 400)
- 1900: Not a leap year (divisible by 100 but not 400)
- 2023: Not a leap year

---

### Negative Number Handling

The library uses `Math.Abs()` on all numeric inputs to handle negative values:

- Converts negative numbers to positive
- Allows flexible API usage
- Ensures consistent results

---

## Notes and Best Practices

1. **Use Accurate Month Calculation**: For critical calculations, use `GetNumOfMonthsFromDays()` with a DateTime parameter rather than the simple version.

2. **Validation First**: Always validate dates before performing calculations using `CheckDate()` or `IsValidBirthDate()`.

3. **TimeSpan Properties**: After getting a TimeSpan from date differences, you can access:
   - `.Days` - Total days
   - `.Hours` - Hours component (0-23)
   - `.Minutes` - Minutes component (0-59)
   - `.Seconds` - Seconds component (0-59)
   - `.TotalDays` - Total as decimal
   - `.TotalHours` - Total as decimal
   - `.TotalMinutes` - Total as decimal
   - `.TotalSeconds` - Total as decimal

4. **Month Calculation Algorithm**: The accurate month calculation iterates through months, so it's slightly slower but much more accurate than a simple division.

5. **DateTimePicker Support**: The `ExtractDateFromDateTimePicker()` method is specifically designed for Windows Forms DateTimePicker controls.

---

**Library Version**: 1.0  
**.NET Framework**: 8.0  
**Last Updated**: July 2026
