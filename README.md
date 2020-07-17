[nuget-icon]:           https://img.shields.io/nuget/v/BigBitMask.NET.svg
[nuget-downloads-icon]: https://img.shields.io/nuget/dt/BigBitMask.NET.svg
[nuget-url]:            https://www.nuget.org/packages/BigBitMask.NET/

[test-icon]:            https://travis-ci.com/ASolomatin/BigBitMask.NET.svg?branch=master
[test-url]:             https://travis-ci.com/ASolomatin/BigBitMask.NET

[coverage-icon]:        https://coveralls.io/repos/github/ASolomatin/BigBitMask.NET/badge.svg?branch=master
[coverage-url]:         https://coveralls.io/github/ASolomatin/BigBitMask.NET

[packaging-icon]:       https://github.com/ASolomatin/BigBitMask.NET/workflows/publish%20to%20nuget/badge.svg
[packaging-url]:        https://github.com/ASolomatin/BigBitMask.NET/actions?query=workflow%3A%22publish+to+nuget%22

[license-icon]:         https://img.shields.io/github/license/ASolomatin/BigBitMask.NET
[license-url]:          https://github.com/ASolomatin/BigBitMask.NET/blob/master/LICENSE

# BigBitMask.NET

[![NuGet][nuget-icon]][nuget-url]
[![NuGet downloads][nuget-downloads-icon]][nuget-url]
[![Travis-CI][test-icon]][test-url]
[![Coverage Status][coverage-icon]][coverage-url]
[![NuGet Package][packaging-icon]][packaging-url]
[![GitHub][license-icon]][license-url]

----------------------------------------

When bits is not enough ...

This library implements a .NET, fully compatible, version of [big-bit-mask](https://github.com/ASolomatin/big-bit-mask) - the bitmask serializable into a base64-like, url-safe string.

## Install
```
> dotnet add package BigBitMask.NET
```

## Usage

### Namespace
```cs
using BigBitMask.NET;
```

### What next?

Now we can create new empty bitmask
```cs
var bitmask = new BitMask();
```
or load it from string
```cs
var bitmask = new BitMask("CE3fG_gE-56");

//Let's see what inside now
var content = "";
for (var i = 0; i < 11 * 6; i++) // Each character contains 6 bits, as in base64
    content += bitmask[i] ? "1" : "0";
Console.WriteLine(content);
```
output: `010000001000111011111110011000111111000001001000011111100111010111`

Then we can change some bits and get back our string representation
```cs
bitmask[65] = false;
bitmask[64] = false;
bitmask[63] = false;
bitmask[61] = false;

bitmask[19] = false;
bitmask[5] = true;

Console.WriteLine(bitmask.ToString());
```
output: `iE3dG_gE-5`

#### But what if I want to have a named flags?

You can extend BitMask class with your model:
```cs
class MyCoolCheckboxes : BitMask
{
    public const int CHECKBOX_0 = 0;
    public const int CHECKBOX_1 = 1;
    public const int CHECKBOX_2 = 2;
    public const int CHECKBOX_3 = 3;
    public const int CHECKBOX_4 = 4;
    public const int CHECKBOX_5 = 5;
    public const int CHECKBOX_6 = 6;
    public const int CHECKBOX_7 = 7;
    public const int CHECKBOX_8 = 8;
    public const int CHECKBOX_9 = 9;

    public bool Checkbox0 { get => this[CHECKBOX_0]; set => this[CHECKBOX_0] = value; }
    public bool Checkbox1 { get => this[CHECKBOX_1]; set => this[CHECKBOX_1] = value; }
    public bool Checkbox2 { get => this[CHECKBOX_2]; set => this[CHECKBOX_2] = value; }
    public bool Checkbox3 { get => this[CHECKBOX_3]; set => this[CHECKBOX_3] = value; }
    public bool Checkbox4 { get => this[CHECKBOX_4]; set => this[CHECKBOX_4] = value; }
    public bool Checkbox5 { get => this[CHECKBOX_5]; set => this[CHECKBOX_5] = value; }
    public bool Checkbox6 { get => this[CHECKBOX_6]; set => this[CHECKBOX_6] = value; }
    public bool Checkbox7 { get => this[CHECKBOX_7]; set => this[CHECKBOX_7] = value; }
    public bool Checkbox8 { get => this[CHECKBOX_8]; set => this[CHECKBOX_8] = value; }
    public bool Checkbox9 { get => this[CHECKBOX_9]; set => this[CHECKBOX_9] = value; }
}
```

or

```cs
class MyCoolCheckboxes : BitMask
{
    public enum Checkboxes
    {
        CHECKBOX_0,
        CHECKBOX_1,
        CHECKBOX_2,
        CHECKBOX_3,
        CHECKBOX_4,
        CHECKBOX_5,
        CHECKBOX_6,
        CHECKBOX_7,
        CHECKBOX_8,
        CHECKBOX_9,
    }

    public bool this[Checkboxes checkboxId] { get => this[(int)checkboxId]; set => this[(int)checkboxId] = value; }

    public bool Checkbox0 { get => this[Checkboxes.CHECKBOX_0]; set => this[Checkboxes.CHECKBOX_0] = value; }
    public bool Checkbox1 { get => this[Checkboxes.CHECKBOX_1]; set => this[Checkboxes.CHECKBOX_1] = value; }
    public bool Checkbox2 { get => this[Checkboxes.CHECKBOX_2]; set => this[Checkboxes.CHECKBOX_2] = value; }
    public bool Checkbox3 { get => this[Checkboxes.CHECKBOX_3]; set => this[Checkboxes.CHECKBOX_3] = value; }
    public bool Checkbox4 { get => this[Checkboxes.CHECKBOX_4]; set => this[Checkboxes.CHECKBOX_4] = value; }
    public bool Checkbox5 { get => this[Checkboxes.CHECKBOX_5]; set => this[Checkboxes.CHECKBOX_5] = value; }
    public bool Checkbox6 { get => this[Checkboxes.CHECKBOX_6]; set => this[Checkboxes.CHECKBOX_6] = value; }
    public bool Checkbox7 { get => this[Checkboxes.CHECKBOX_7]; set => this[Checkboxes.CHECKBOX_7] = value; }
    public bool Checkbox8 { get => this[Checkboxes.CHECKBOX_8]; set => this[Checkboxes.CHECKBOX_8] = value; }
    public bool Checkbox9 { get => this[Checkboxes.CHECKBOX_9]; set => this[Checkboxes.CHECKBOX_9] = value; }
}
```

and use it

```cs
var checkboxes = new MyCoolCheckboxes();
checkboxes.Checkbox5 = true;
checkboxes.Checkbox7 = true;
checkboxes.Checkbox8 = true;

Console.WriteLine(checkboxes.ToString());
```

output: `gG`

----------------------------------------

## License

**[MIT][license-url]**

Copyright (C) 2020 Aleksej Solomatin