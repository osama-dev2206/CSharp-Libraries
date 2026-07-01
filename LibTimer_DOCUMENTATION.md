# LibTimer Library - Complete Documentation

## Table of Contents
1. [Overview](#overview)
2. [Architecture](#architecture)
3. [Components](#components)
4. [API Reference](#api-reference)
5. [Usage Examples](#usage-examples)
6. [Design Patterns](#design-patterns)
7. [Implementation Details](#implementation-details)

---

## Overview

The **LibTimer** library is a lightweight, efficient timer library designed for managing time components (hours, minutes, seconds) in C# applications. It provides a sealed class implementation with a well-defined interface for predictable and secure timer operations.

- **Target Framework**: .NET 8.0
- **Namespace**: `LibTimer`
- **Main Class**: `clsTimerCore` (sealed class)
- **Interface**: `ICore` (internal interface)
- **Type**: Stateful component management

### Key Capabilities
- Time component storage (hours 0-23, minutes 0-59, seconds 0-59)
- Individual component update methods with automatic wrapping
- Reset functionality for each time component
- Property-based access to time values
- Sealed implementation for performance and security

---

## Architecture

### Class Structure
```
LibTimer
├── ICore (Interface - defines contract)
├── clsTimerCore (Sealed implementation)
│   ├── Properties (Min, Sec, Hour)
│   ├── Constructor (Initialization)
│   ├── Update Methods (UpdateSec, UpdateMin, UpdateHour)
│   └── Reset Methods (RestSec, RestMin, RestHour)
```

### Design Characteristics
- **Sealed Class**: `clsTimerCore` cannot be inherited
- **Internal Interface**: `ICore` is internal to the namespace
- **Auto-wrapping**: Values automatically wrap when exceeding limits (59 → 0, 23 → 0)
- **Immutable Properties**: Public properties are read-only (private setters)

---

## Components

### Interface: ICore

**Namespace**: `LibTimer`  
**Access**: `internal interface`  
**Purpose**: Defines the contract for timer core functionality

#### ICore Members

**Properties** (Read-only):
```csharp
int Min { get; }
int Sec { get; }
int Hour { get; }
```

**Methods**:
```csharp
void UpdateMin();
void UpdateHour();
void UpdateSec();
void RestSec();
void RestMin();
void RestHour();
```

---

### Class: clsTimerCore

**Namespace**: `LibTimer`  
**Access**: `public sealed class`  
**Implements**: `ICore`  
**Purpose**: Sealed implementation of timer core with time component management

#### Key Characteristics
- **Sealed**: Cannot be inherited by other classes
- **Stateful**: Maintains internal state of time components
- **Thread-unsafe**: Not designed for multi-threaded access
- **Zero-dependency**: No external dependencies required

---

## API Reference

### Properties

#### Min Property
```csharp
public int Min { get; private set; }
```
**Description**: Gets the current minutes value.

**Access**: 
- Get: Public (anyone can read)
- Set: Private (only internal to class)

**Range**: 0-59

**Default Value**: 0

**Characteristics**:
- Read-only from external code
- Wraps from 59 to 0 when incremented beyond 59
- Reset to 0 using `RestMin()`

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();
int currentMin = timer.Min;  // Accessing the value
```

---

#### Sec Property
```csharp
public int Sec { get; private set; }
```
**Description**: Gets the current seconds value.

**Access**: 
- Get: Public
- Set: Private

**Range**: 0-59

**Default Value**: 0

**Characteristics**:
- Read-only from external code
- Wraps from 59 to 0 when incremented beyond 59
- Reset to 0 using `RestSec()`

**Example**:
```csharp
int currentSec = timer.Sec;
```

---

#### Hour Property
```csharp
public int Hour { get; private set; }
```
**Description**: Gets the current hours value.

**Access**: 
- Get: Public
- Set: Private

**Range**: 0-23 (24-hour format)

**Default Value**: 0

**Characteristics**:
- Read-only from external code
- Wraps from 23 to 0 when incremented beyond 23
- Reset to 0 using `RestHour()`

**Example**:
```csharp
int currentHour = timer.Hour;
```

---

### Constructor

#### clsTimerCore()
```csharp
public clsTimerCore()
```
**Description**: Initializes a new instance of the timer with all time components set to zero.

**Parameters**: None

**Initialization**:
- `Min` = 0
- `Sec` = 0
- `Hour` = 0

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();  // Creates timer at 00:00:00
```

---

### Update Methods

#### UpdateSec()
```csharp
public void UpdateSec()
```
**Description**: Increments the seconds by one. Wraps from 59 to 0 automatically.

**Behavior**:
- If `Sec < 59`: Increment by 1
- If `Sec == 59`: Reset to 0

**Side Effects**: Modifies internal `Sec` property only (does not affect minutes or hours)

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();

// Incrementing seconds
for (int i = 0; i < 60; i++)
{
    timer.UpdateSec();
}
// After loop: Sec = 0 (wrapped around)

// Increment once more
timer.UpdateSec();
// Now: Sec = 1
```

---

#### UpdateMin()
```csharp
public void UpdateMin()
```
**Description**: Increments the minutes by one. Wraps from 59 to 0 automatically.

**Behavior**:
- If `Min < 59`: Increment by 1
- If `Min == 59`: Reset to 0

**Comment in Code**: "min less than 60 then increment it"

**Side Effects**: Modifies internal `Min` property only (does not affect seconds or hours)

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();

// Incrementing minutes
for (int i = 0; i < 60; i++)
{
    timer.UpdateMin();
}
// After loop: Min = 0 (wrapped around)
```

---

#### UpdateHour()
```csharp
public void UpdateHour()
```
**Description**: Increments the hours by one. Wraps from 23 to 0 automatically.

**Behavior**:
- If `Hour < 23`: Increment by 1
- If `Hour == 23`: Call `RestHour()` (resets to 0)

**24-Hour Format**: Maintains 24-hour (0-23) time format

**Side Effects**: Modifies internal `Hour` property only

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();

// Incrementing to 23 hours
for (int i = 0; i < 23; i++)
{
    timer.UpdateHour();
}
// Hour = 23

timer.UpdateHour();  // Wraps around
// Hour = 0
```

---

### Reset Methods

#### RestSec()
```csharp
public void RestSec()
```
**Description**: Resets the seconds to 0.

**Parameters**: None

**Effect**: Sets `Sec` property to 0

**Note**: The method name uses "Rest" instead of "Reset" (likely a naming convention in the codebase)

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();
timer.UpdateSec();
timer.UpdateSec();
timer.UpdateSec();  // Sec = 3

timer.RestSec();    // Reset to 0
// Sec = 0
```

---

#### RestMin()
```csharp
public void RestMin()
```
**Description**: Resets the minutes to 0.

**Parameters**: None

**Effect**: Sets `Min` property to 0

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();

// Set to 45 minutes
for (int i = 0; i < 45; i++)
{
    timer.UpdateMin();
}
// Min = 45

timer.RestMin();
// Min = 0
```

---

#### RestHour()
```csharp
public void RestHour()
```
**Description**: Resets the hours to 0.

**Parameters**: None

**Effect**: Sets `Hour` property to 0

**Example**:
```csharp
clsTimerCore timer = new clsTimerCore();

// Set to 15 hours
for (int i = 0; i < 15; i++)
{
    timer.UpdateHour();
}
// Hour = 15

timer.RestHour();
// Hour = 0
```

---

## Usage Examples

### Example 1: Creating and Initializing a Timer
```csharp
using LibTimer;

// Create a new timer instance
clsTimerCore timer = new clsTimerCore();

// All components start at 0
Console.WriteLine($"Time: {timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}");  
// Output: Time: 00:00:00
```

---

### Example 2: Incrementing Seconds
```csharp
using LibTimer;

clsTimerCore timer = new clsTimerCore();

// Increment seconds for a simple countdown
for (int i = 0; i < 10; i++)
{
    timer.UpdateSec();
    Console.WriteLine($"Time: {timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}");
}
// Output: 00:00:01 through 00:00:10
```

---

### Example 3: Building a Simple Timer Loop
```csharp
using LibTimer;
using System;
using System.Threading;

clsTimerCore timer = new clsTimerCore();

// Run a simple 1-minute timer
while (timer.Min < 1)
{
    Console.Clear();
    Console.WriteLine($"Elapsed: {timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}");
    
    timer.UpdateSec();
    Thread.Sleep(1000);  // Wait 1 second
}

Console.WriteLine("1 minute elapsed!");
```

---

### Example 4: Handling Second Wraparound
```csharp
using LibTimer;

clsTimerCore timer = new clsTimerCore();

// Advance to 59 seconds
for (int i = 0; i < 59; i++)
{
    timer.UpdateSec();
}

Console.WriteLine($"Before: {timer.Sec}");  // 59

timer.UpdateSec();  // Wraparound

Console.WriteLine($"After: {timer.Sec}");   // 0
// Note: This does NOT automatically increment minutes
```

---

### Example 5: Complete 24-Hour Cycle
```csharp
using LibTimer;

clsTimerCore timer = new clsTimerCore();

// Demonstrate hour wraparound
for (int i = 0; i < 24; i++)
{
    timer.UpdateHour();
}

Console.WriteLine($"After 24 increments, Hour = {timer.Hour}");  // 0
```

---

### Example 6: Resetting Individual Components
```csharp
using LibTimer;

clsTimerCore timer = new clsTimerCore();

// Build up time
for (int i = 0; i < 30; i++) timer.UpdateSec();
for (int i = 0; i < 45; i++) timer.UpdateMin();
for (int i = 0; i < 12; i++) timer.UpdateHour();

Console.WriteLine($"Full time: {timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}");  
// Output: 12:45:30

// Reset seconds only
timer.RestSec();
Console.WriteLine($"After reset seconds: {timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}");  
// Output: 12:45:00

// Reset minutes
timer.RestMin();
Console.WriteLine($"After reset minutes: {timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}");  
// Output: 12:00:00

// Reset hours
timer.RestHour();
Console.WriteLine($"After reset hours: {timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}");  
// Output: 00:00:00
```

---

### Example 7: Display Timer in Different Formats
```csharp
using LibTimer;

clsTimerCore timer = new clsTimerCore();

// Set up a sample time
for (int i = 0; i < 5; i++) timer.UpdateSec();
for (int i = 0; i < 30; i++) timer.UpdateMin();
for (int i = 0; i < 14; i++) timer.UpdateHour();

// Display formats
string format1 = $"{timer.Hour:D2}:{timer.Min:D2}:{timer.Sec:D2}";
string format2 = $"{timer.Hour}h {timer.Min}m {timer.Sec}s";
string format3 = $"{timer.Hour * 3600 + timer.Min * 60 + timer.Sec} seconds total";

Console.WriteLine(format1);  // 14:30:05
Console.WriteLine(format2);  // 14h 30m 5s
Console.WriteLine(format3);  // 52205 seconds total
```

---

## Design Patterns

### 1. **Encapsulation Pattern**
- Properties are read-only from external code
- Internal state managed through public methods only
- `private set` prevents direct modification

```csharp
// External code:
int min = timer.Min;      // Allowed
timer.Min = 10;           // Compile error - private set
timer.UpdateMin();        // Correct way
```

---

### 2. **Sealed Class Pattern**
- `clsTimerCore` is sealed to prevent inheritance
- Guarantees behavior cannot be altered through subclassing
- Enables certain optimizations in the runtime
- Provides security guarantees for critical functionality

```csharp
// This would fail to compile:
// public class MyTimer : clsTimerCore { }  // Error: sealed class
```

---

### 3. **Auto-wrapping Pattern**
- Time components automatically wrap at boundaries
- Minutes: 0-59 wrapping
- Hours: 0-23 wrapping
- Simplifies caller code (no need for manual modulo operations)

```csharp
// The library handles wrapping internally
for (int i = 0; i < 65; i++) timer.UpdateSec();
// Caller doesn't need to manage: Sec will be properly wrapped
```

---

### 4. **Atomic State Updates**
- Each method updates a single component
- No cascading updates (seconds don't auto-increment minutes)
- Caller controls component advancement

```csharp
// Seconds wrapping to 0 does NOT auto-increment minutes
// This is by design - caller has full control
```

---

## Implementation Details

### Property Implementation
```csharp
public int Min { get; private set; }
public int Sec { get; private set; }
public int Hour { get; private set; }
```

**Access Modifiers**:
- `public get` - Anyone can read the value
- `private set` - Only the class can set the value

**Storage**: Backed by implicit auto-properties

---

### Update Method Logic

#### UpdateSec Logic Pattern
```
IF Sec < 59 THEN
    Sec = Sec + 1
ELSE
    Sec = 0
END IF
```

#### UpdateMin Logic Pattern
```
IF Min < 59 THEN
    Min = Min + 1
ELSE
    Min = 0
END IF
```

#### UpdateHour Logic Pattern
```
IF Hour < 23 THEN
    Hour = Hour + 1
ELSE
    Call RestHour()
END IF
```

---

### Boundary Values

| Component | Min | Max | Wrap Behavior |
|-----------|-----|-----|---------------|
| Seconds | 0 | 59 | 59 → 0 |
| Minutes | 0 | 59 | 59 → 0 |
| Hours | 0 | 23 | 23 → 0 |

---

### State Initialization
- Constructor sets all components to 0
- No lazy initialization
- No asynchronous initialization required
- Ready to use immediately after construction

```csharp
clsTimerCore timer = new clsTimerCore();
// Immediately usable:
// timer.Hour = 0
// timer.Min = 0
// timer.Sec = 0
```

---

## Notes and Best Practices

1. **Manual Component Synchronization**: 
   - The library does NOT automatically synchronize components
   - Incrementing seconds to 59 then once more sets seconds to 0, but does NOT increment minutes
   - This is by design for maximum flexibility
   - Caller is responsible for managing cascading increments

2. **No Automatic Time Advancement**:
   - The library does not automatically track real time
   - Methods must be called explicitly to advance time
   - Suitable for game timers, countdowns, or custom timing logic

3. **Format Display**:
   - Use `:D2` format specifier for zero-padded display
   - Example: `$"{hour:D2}:{min:D2}:{sec:D2}"` → "14:05:03"

4. **Single Threading**:
   - Class is not thread-safe
   - Access from multiple threads may cause race conditions
   - Lock if needed in multi-threaded scenarios

5. **Usage Pattern**:
   - Create instance: `new clsTimerCore()`
   - Loop or event handler: call `UpdateSec()`, `UpdateMin()`, or `UpdateHour()`
   - Read properties: `timer.Hour`, `timer.Min`, `timer.Sec`
   - Reset as needed: `RestSec()`, `RestMin()`, `RestHour()`

6. **Performance**:
   - Sealed class allows compiler optimizations
   - Direct property access (no virtual dispatch)
   - Minimal memory footprint (3 integers)
   - Very fast method calls

7. **Common Patterns**:
   - **Stopwatch**: Increment all three in nested loops for elapsed time
   - **Countdown**: Use in descending loop with negative logic
   - **Periodic updates**: Call from timer events (System.Timers.Timer)
   - **Display formatting**: Use format specifiers for clock display

---

## Limitations and Considerations

1. **No Built-in Cascading**: You must manually increment minutes when seconds wrap
2. **No Time Change Notifications**: No events when values change
3. **No Persistence**: State is lost when instance is garbage collected
4. **No Thread Safety**: Requires external synchronization for multi-threaded use
5. **24-Hour Format Only**: No AM/PM conversion utilities included
6. **No Validation**: No input validation for external manipulation (if reflection is used)

---

**Library Version**: 1.0  
**.NET Framework**: 8.0  
**Last Updated**: July 2026
