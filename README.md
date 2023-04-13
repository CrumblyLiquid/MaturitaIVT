# MaturitaIVT
## Výměna prvků v poli:

`(array[j], array[i]) = (array[i], array[j]);`

## Binární strom
rodič: `(n-1)/2`

levé dítě: `2*n + 1`

pravé dítě: `2*n + 2`

## Mocnění:

`Math.Pow(-1.0, n)`

## Absolutní hodnota:

`Math.Abs(pi - last)`

## Faktoriál

```
static int Factorial(int num)
{
    if (num == 0)
        return 1;

    return num * Factorial(num - 1);
}
```

## Využití abecedního stringu pro získání hodnoty znaku:

```
const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";

// Převod celých čísel
static string Whole(int num, int b)
{
    string res = "";

    while (num > 0)
    {
        res = alphabet[num % b] + res;
        num /= b;
    }

    return res;
}
```

## Zjištění, jestli slovník obsahuje klíč:

`dict.ContainsKey(c)`

## Seřazení slovníku:

`foreach (var kvp in dict.OrderByDescending(x => x.Value))`

## Multidimenzionální pole

`int\[,] m = new int\[3,2];` ## 3 řádky, 2 sloupce (2 dimenze)
počet řádků: `m.GetLength(0)`
počet sloupců: `m.GetLength(1)`
`m\[řádek, sloupec]`
