using System.Text.RegularExpressions;

namespace Programovani
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sorting();
            Console.WriteLine(ApproxPi(0.00001));
            Console.WriteLine(ApproxSin(-1696.0, 0.0001));
            Console.WriteLine(ApproxCos(-100.0, 0.0001).ToString("F99").TrimEnd('0'));
            Console.WriteLine(ApproxSqrt(29.0, 3.0, 0.001));
            Console.WriteLine(DecimalToAny(308.3m, 3));
            Console.WriteLine(AnyToDecimal("102102", 3));
            FrequencyAnalyzer("AHOJ, JAK SE MAS, KAMARADE?");
            FileFrequencyAnalyzer("../../../Program.cs");
            Console.WriteLine(MorseEncode("Ahoj, jak se mas?"));
            Console.WriteLine(MorseDecode(".-/..../---/.---//.---/.-/-.-//..././/--/.-/.../"));
            MatrixMultiplication();
            MatrixDeterminant();
            MatrixRotation();
            Console.WriteLine(Euclide(140, 15));
        }

        static void Sorting()
        {
            int[] array = { 17, 9, 1, 56, 9, 3, 0, 12 };
            // int[] array = { 17, 9, 1};

            InsertionSort(array);
            SelectionSort(array);
            BubbleSort(array);
            ShakerSort(array);
            QuickSort(array);
            HeapSort(array);
            MergeSort(array);

            Console.WriteLine(string.Join(", ", array));
        }

        /// <summary>
        /// 1. Insertion Sort
        /// Čas: 6m45s
        /// První prvek bereme jako seřazený
        /// Další prvek (prvky) posouváme dokud nenajdeme jeho místo v seřazených prvních
        /// Vizualizace: https://www.algoritmy.net/article/8/Insertion-sort
        /// </summary>
        /// <param name="array">Pole, které chceme seřadit</param>
        static void InsertionSort(int[] array)
        {
            // Prochází celé pole (první prvek se bere jako "triviálně" seřazený)
            for (int i = 1; i < array.Length; i++)
            {
                // Posouvá prvek dokud nenajde jeho místo v seřazených prvcích
                // Seřazené prvky jsou všechny prvky před i, jelikož ty už jsme seřadili dříve
                for (int j = i; j > 0; j--)
                {
                    if (array[j] > array[j - 1])
                        // Prohodí 2 hodnoty v poli
                        (array[j], array[j - 1]) = (array[j - 1], array[j]);
                    else
                        break;
                }
            }
        }

        /// <summary>
        /// 2. Selection Sort
        /// Čas: 7m16s
        /// Postupně vybíráme nejvyšší prvky z neseřazené části pole a umisťujeme je na konec seřazené části.
        /// Vizualizace: https://www.algoritmy.net/article/4/Selection-sort
        /// </summary>
        /// <param name="array">Pole, které chceme seřadit</param>
        static void SelectionSort(int[] array)
        {
            // Postupně postupujeme polem a zvětšujeme seřazenou část
            // unsorted je index kde začíná neseřazená část pole
            for (int unsorted = 0; unsorted < array.Length; unsorted++)
            {
                // Najdeme nejvyšší prvek v neseřazené části
                int biggestIndex = unsorted;
                for (int i = unsorted + 1; i < array.Length; i++)
                {
                    if (array[i] >= array[biggestIndex])
                        biggestIndex = i;
                }
                // Prohodíme nejvyšší prvek v neseřazené části s první prvek neseřazené části
                // Tedy zvětšíme seřazenou část o jeden prvek
                (array[unsorted], array[biggestIndex]) = (array[biggestIndex], array[unsorted]);
            }
        }

        /// <summary>
        /// 3. Bubble Sort
        /// Čas: 2m10s
        /// Porovnává dva sousední prvky, a pokud je nižší číslo nalevo od vyššího, tak je prohodí
        /// (nižší číslo je lehčí a rychleji stoupá ke konci pole - jako bublina)
        /// Na konci iterace se tímto způsobem na konec pole vždy dostane ta nejlehčí bublinka (nejnižší číslo)
        /// Nyní algoritmus můžeme pustit znovu na redukovaný problém (na poslední pozici pole je již to správné číslo)
        /// Vizualizace: https://www.algoritmy.net/article/3/Bubble-sort
        /// </summary>
        /// <param name="array">Pole, které chceme seřadit</param>
        static void BubbleSort(int[] array)
        {
            // Projede vždy neseřazenou část pole
            for (int lenght = array.Length; lenght > 0; lenght--)
            {
                // Nižší prvky posouvá blíž ke konci pole
                for (int i = 0; i < lenght - 1; i++)
                {
                    // Prohodí prvky pokud je prvek nalevo menší než prvek napravo
                    if (array[i] < array[i + 1])
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                }
            }
        }

        /// <summary>
        /// 4. Bubble Sort
        /// Čas: 3m01s
        /// Funguje stejně jako Bubble Sort ale "bublá" nejlehčí prvky na konec a nejtěžší na začátek
        /// Vizualizace: https://www.algoritmy.net/article/93/Shaker-sort
        /// </summary>
        /// <param name="array">Pole, které chceme seřadit</param>
        static void ShakerSort(int[] array)
        {
            // Pokud jsou start a end vedle sebe, máme seřazené celé pole jelikož seřazené části jsou 0-start a end-(array.Length-1)
            int start = 0;
            int end = array.Length - 1;
            while (start < end)
            {
                // Probublávání lehčích prvků na konec
                for (int i = start; i < end; i++)
                {
                    if (array[i] < array[i + 1])
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                }
                end--;

                // Probublávání těžších prvků na začátek
                for (int i = end; i > start; i--)
                {
                    if (array[i] > array[i - 1])
                        (array[i], array[i - 1]) = (array[i - 1], array[i]);
                }
                start++;
            }
        }

        /// <summary>
        /// 5. Quick Sort
        /// Čas: 5m00s
        /// Zvolíme libovolný prvek (například poslední) a řadíme prvky na podle toho jestli jsou větší nebo menší než pivot
        /// Tak nám vzniknou 3 části: větší než pivot, samotný pivot, menší než pivot
        /// Na část větší a menší než pivot znovu spustíme quick sort
        /// Vizualizace: https://www.algoritmy.net/article/10/Quicksort
        /// </summary>
        /// <param name="array">Pole, které chceme seřadit</param>
        static void QuickSort(int[] array)
        {
            static void QuickSortInner(int[] array, int start, int end)
            {
                // Nemá cenu řadit pole o 1 prvku
                if (start >= end)
                    return;

                // Zvolíme si pivota
                int pivot = end;
                // `moved` je index na který má přijít další prvek, který je větší než pivot a nakonec i sám
                int moved = start;
                // Projdeme specifikovanou část pole a roztřídíme podle velikosti vůči pivotu
                for (int i = start; i < end; ++i)
                {
                    if (array[i] > array[pivot])
                    {
                        (array[i], array[moved]) = (array[moved], array[i]);
                        moved++;
                    }
                }
                // Dáme pivot na správné místo
                (array[pivot], array[moved]) = (array[moved], array[pivot]);

                // Spustíme quick sort na neseřazené části
                QuickSortInner(array, start, moved - 1);
                QuickSortInner(array, moved + 1, end);
            }

            QuickSortInner(array, 0, array.Length - 1);
        }

        /// <summary>
        /// 6. Heap Sort
        /// Čas: 20m18s
        /// Nejdříve vytvoříme haldu (binární strom kde rodič je vždy větší než oba jeho potomci)
        /// Poté vždy vyměníme první (největší) a poslední prvek v haldě a zmenšíme haldu o 1 a znova ji "dorovnáme"
        /// Opakujeme dokud halda není prázdná (tedy dokud jsme všechny prvky nepřesunuli na konec)
        /// Vizualizace: https://www.algoritmy.net/article/17/Heapsort
        /// </summary>
        /// <param name="array">Pole, které chceme seřadit</param>
        static void HeapSort(int[] array)
        {
            // Začne vytvářet hladu od daného indexu a v daném rozsahu pole
            static void Heapify(int[] array, int node, int length)
            {
                // Zjistí největší prvek z trojice rodič, levé dítě, pravé dítě
                int leftChild = 2 * node + 1;
                int rightChild = 2 * node + 2;

                int max = node;

                if (leftChild < length && array[leftChild] > array[max])
                    max = leftChild;
                if (rightChild < length && array[rightChild] > array[max])
                    max = rightChild;

                // Pokud potřeba, prohodí větší dítě s rodičem a "dorovná" haldu pod tím indexem, který byl prohozen
                if (max != node)
                {
                    (array[max], array[node]) = (array[node], array[max]);
                    Heapify(array, max, length);
                }
            }

            // Vypočítáme posledního rodiče
            // (šlo by to zjednodušit, ale pro čitelnost to ponechávám takto)
            int lastItem = array.Length - 1;
            int lastParent = (lastItem - 1) / 2;

            // Vytvoříme haldu
            for (int i = lastParent; i >= 0; --i)
            {
                Heapify(array, i, array.Length);
            }

            // Vždy vezmeme největší prvek na vrchu haldy a přesuneme ho na konec
            // Znovu dotvoříme haldu ze zbývájící části pole
            for (int i = array.Length - 1; i >= 0; --i)
            {
                (array[0], array[i]) = (array[i], array[0]);
                Heapify(array, 0, i);
            }
        }

        /// <summary>
        /// 7. Merge Sort
        /// Čas: 11m45s
        /// Pole postupně dělíme až dojdeme k části pole o pouze 1 prvku
        /// Poté pole postupně znovu sléváme až dojdeme k seřazenému poli
        /// Vizualizace: https://www.algoritmy.net/article/13/Merge-sort
        /// </summary>
        /// <param name="array">Pole, které chceme seřadit</param>
        static void MergeSort(int[] array)
        {
            // Rozdělí pole na 2 (na nich spustí sama sebe) a potom je "sleje" do jednoho pole
            static void MergeSortInner(int[] array, int start, int end)
            {
                if (start == end)
                    return;

                int half = (end + start) / 2;

                MergeSortInner(array, start, half);
                MergeSortInner(array, half + 1, end);

                Merge(array, start, half, end);
            }

            // Sleje 2 části pole do jednoho podle velikosti prvků
            static void Merge(int[] array, int start, int half, int end)
            {
                // Dočasné pole použité k jednoduššímu slévání polí
                List<int> temp = new List<int>();

                // Víme, že každá část pole je seřazená podle velikost, tedy první prvky jsou největší, atd.
                // Tedy vybíráme vždy větší prvek mezi 2 částmi pole
                int left = start;
                int right = half + 1;
                while (left <= half && right <= end)
                {
                    if (array[left] > array[right])
                    {
                        temp.Add(array[left]);
                        left++;
                    }
                    else
                    {
                        temp.Add(array[right]);
                        right++;
                    }
                }

                // Pokud jsme jednu část pole vyčerpali (celou ji přesunuli do temp)
                // musíme přesunout zbytek druhé části
                int s = left;
                int e = half;
                if (right <= end)
                {
                    s = right;
                    e = end;
                }

                while (s <= e)
                {
                    temp.Add(array[s]);
                    s++;
                }

                // Propíšeme seřazené temp do původního pole
                for (int i = start; i <= end; i++)
                {
                    array[i] = temp[i - start];
                }
            }

            MergeSortInner(array, 0, array.Length - 1);
        }

        /// <summary>
        /// 8. Program na výpočet Ludolfova čísla s danou přesností
        /// Čas: 7m63s
        /// Aproximace hodnoty pi
        /// </summary>
        /// <param name="d">Přesnost pi</param>
        /// <returns>Přibližná hodnota pi</returns>
        static double ApproxPi(double d)
        {
            double last;
            double pi = 0.0;

            double n = 0;
            do
            {
                last = pi;
                // Násobím 4 zde, aby můj výsledek opravdu odpovídal zadané přesnosti
                pi += 4.0 * Math.Pow(-1.0, n) * (1.0 / (2.0 * n + 1.0));
                n++;
            }
            // Pokračujeme s přičítáním dokud rozdíl posledních dvou hodnot není menší než d (tedy jsme dosáhli žádané přesnosti)
            while (Math.Abs(pi - last) > d);

            return pi;
        }

        /// <summary>
        /// 9. Aproximace funkce sinus v bodě x
        /// Čas: 23m18s
        /// Aproximace hodnoty sin(x)
        /// </summary>
        /// <param name="angleDeg">Úhel ve stupních</param>
        /// <param name="precision">Přesnost sin(x)</param>
        /// <returns>Přibližná hodnota funkce sinus v bodě x</returns>
        static double ApproxSin(double angleDeg, double precision)
        {
            static int Factorial(int num)
            {
                if (num == 0)
                    return 1;

                return num * Factorial(num - 1);
            }

            // Převod stupňů na radiány
            double angleRad = angleDeg * Math.PI / 180;

            // Konverze na pozitivní stupně
            double positiveAngleRad = angleRad > 0 ? angleRad : -angleRad;

            // Zmenšení úhlu na velikost do 360°
            double circleAngle = positiveAngleRad;
            if (circleAngle > 2 * Math.PI)
                circleAngle = circleAngle % 2 * Math.PI;

            // Zmenšení úhlu na velikost do 180°
            double halfCircle = circleAngle;
            if (halfCircle > Math.PI)
                halfCircle -= Math.PI;

            // Zmenšení úhlu na velikost do 90°
            double quarterCircle = halfCircle;
            if (quarterCircle * 2 > Math.PI)
                quarterCircle = Math.PI - quarterCircle;

            // Vypočteme hodnotu sinu do dané přesnosti
            double last;
            double sin = 0.0;

            int n = 0;
            do
            {
                last = sin;
                sin += (
                    Math.Pow(-1.0, n)
                    * Math.Pow(quarterCircle, (2.0 * n + 1.0))
                    / Factorial(2 * n + 1)
                );
                n++;
            } while (Math.Abs(last - sin) > precision);

            // Nastavení správného znaménka, podle kvadrantu a znaménka původního úhlu
            if (circleAngle > Math.PI)
                sin = -sin;

            if (angleRad < 0)
                sin = -sin;

            return sin;
        }

        /// <summary>
        /// 10. Aproximace funkce cosinus v bodě x
        /// Čas: 20m26s
        /// Aproximace hodnoty cos(x)
        /// Funguje prakticky stejně jako sin(x) akorát zjednodušení na úhel do 90° je trochu jiný
        /// </summary>
        /// <param name="angleDeg">Úhel ve stupních</param>
        /// <param name="precision">Přesnost cos(x)</param>
        /// <returns>Přibližná hodnota funkce cosinus v bodě x</returns>
        static double ApproxCos(double angleDeg, double precision)
        {
            static int Factorial(int num)
            {
                return num == 0 ? 1 : num * Factorial(num - 1);
            }

            double angleRad = angleDeg * Math.PI / 180;

            angleRad = angleRad < 0 ? -angleRad : angleRad;

            double circleAngle = angleRad;
            if (circleAngle > 2 * Math.PI)
                circleAngle = circleAngle % (2 * Math.PI);

            double halfCircle = circleAngle;
            if (halfCircle > Math.PI)
                halfCircle = (2 * Math.PI) - halfCircle;

            double quarterCircle = halfCircle;
            if (quarterCircle * 2 > Math.PI)
                quarterCircle = Math.PI - quarterCircle;

            double last;
            double cos = 0.0;

            int n = 0;
            do
            {
                last = cos;
                cos +=
                    ((double)Math.Pow(-1, n) * Math.Pow(quarterCircle, 2 * n))
                    / (double)Factorial(2 * n);
                n++;
            } while (Math.Abs(last - cos) > precision);

            if (halfCircle * 2 > Math.PI)
                cos = -cos;

            return cos;
        }

        /// <summary>
        /// 11. Aproximace n-té odmocniny
        /// Čas: 7m12s
        /// </summary>
        /// <param name="a">Číslo, které chceme odmocnit</param>
        /// <param name="k">Stupeň odmocniny</param>
        /// <param name="d">Přesnost</param>
        /// <returns>Hodnota odmocniny</returns>
        static double ApproxSqrt(double a, double k, double d)
        {
            double last;
            double sqrt = 1.0;

            do
            {
                last = sqrt;
                sqrt = ((k - 1) * last + (a / Math.Pow(last, (k - 1)))) / k;
            } while (Math.Abs(last - sqrt) > d);

            return sqrt;
        }

        /// <summary>
        /// 12. Převod čísel z desítkové soustavy do jiné číselné soustavy
        /// Čas: ~18m
        /// </summary>
        /// <param name="dec">Číslo, které chceme převést</param>
        /// <param name="b">Základ číselné soustavy</param>
        /// /// <returns>Číslo v číselné soustavě dané parametrem `b`</returns>
        static string DecimalToAny(decimal dec, int b)
        {
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

            // Převod desetinných čísel
            static string Decimal(decimal num, int b)
            {
                string res = "";

                while (num > 0 && Regex.Matches(res, @"\b(\d+)\1+\b").Count < 1)
                {
                    num *= b;
                    res += alphabet[(int) num];
                    num -= (int) num;
                }

                return res;
            }

            return Whole((int) dec, b) + "." + Decimal(dec - (int) dec, b);
        }

        /// <summary>
        /// 13. Převod čísel z libovolné číselné soustavy do desítkové soustavy
        /// Čas: 4m47s
        /// </summary>
        /// <param name="number">Číslo, které chceme převést</param>
        /// <param name="b">Základ číselné soustavy</param>
        /// <returns>Číslo v desítkové soustavě</returns>
        static int AnyToDecimal(string number, int b)
        {
            // Zajistíme, aby všechna písmena byla mála, abychom mohli používat `alphabet` ke zjištění hodnty
            number = number.ToLower();

            const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
            
            int res = 0;
            int position = 0; // Číslo dané pozice
            foreach(char c in number.ToCharArray().Reverse())
            {
                // Přidá hodnotu dané pozice k celkovému výsledku
                res += alphabet.IndexOf(c) * (int) Math.Pow(b, position);
                position++;
            }

            return res;
        }

        /// <summary>
        /// 14. Frekvenční analýza
        /// Čas: 4m12s
        /// </summary>
        /// <param name="text">Text, který chceme zanalyzovat</param>
        static void FrequencyAnalyzer(string text)
        {
            text = text.ToLower().Replace("\n", "").Replace("\r", "");

            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach(char c in text.ToCharArray())
            {
                if (dict.ContainsKey(c))
                    dict[c]++;
                else
                    dict[c] = 1;
            }

            foreach (var kvp in dict.OrderByDescending(x => x.Value))
                Console.WriteLine($"`{kvp.Key}`: {kvp.Value} ({Math.Round((double)kvp.Value / (double)text.Length * 100.0, 2)}%)");
        }

        static void FileFrequencyAnalyzer(string path)
        {
            try
            {
                TextReader reader = new StreamReader(path);
                string text = reader.ReadToEnd();

                FrequencyAnalyzer(text);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Nepodařilo se najít požadovaný soubor.");
            }
        }

        /// <summary>
        /// 15. Morseova abeceda - zakódování
        /// Čas: 11m28s
        /// </summary>
        /// <param name="text">Text, který chceme zakódovat</param>
        /// <returns>Zakódovaný text</returns>
        static string MorseEncode(string text)
        {
            Dictionary<char, string> dict = new Dictionary<char, string>()
            {
                {'a', ".-"},
                {'b', "-..."},
                {'c', "-.-."},
                {'d', "-.."},
                {'e', "."},
                {'f', "..-."},
                {'g', "--."},
                {'h', "...."},
                {'i', ".."},
                {'j', ".---"},
                {'k', "-.-"},
                {'l', ".-.."},
                {'m', "--"},
                {'n', "-."},
                {'o', "---"},
                {'p', ".--."},
                {'q', "--.-"},
                {'r', ".-."},
                {'s', "..."},
                {'t', "-"},
                {'u', "..-"},
                {'v', "...-"},
                {'w', ".--"},
                {'x', "-..-"},
                {'y', "-.--"},
                {'z', "--.."},
                {' ', ""},
            };

            string result = "";

            foreach (char c in text.ToLower().ToCharArray())
            {
                if (dict.ContainsKey(c))
                    result += dict[c] + "/";
            }

            return result;
        }

        /// <summary>
        /// 16. Morseova abeceda - dekódování
        /// Čas: 3m4s
        /// </summary>
        /// <param name="morse_text">Text, který chceme dekódovat</param>
        /// <returns>Dekódovaný texté</returns>
        static string MorseDecode(string morse_text)
        {
            Dictionary<string, char> dict = new Dictionary<string, char>()
            {
                {".-", 'a'},
                {"-...", 'b'},
                {"-.-.", 'c'},
                {"-..", 'd'},
                {".", 'e'},
                {"..-.", 'f'},
                {"--.", 'g'},
                {"....", 'h'},
                {"..", 'i'},
                {".---", 'j'},
                {"-.-", 'k'},
                {".-..", 'l'},
                {"--", 'm'},
                {"-.", 'n'},
                {"---", 'o'},
                {".--.", 'p'},
                {"--.-", 'q'},
                {".-.", 'r'},
                {"...", 's'},
                {"-", 't'},
                {"..-", 'u'},
                {"...-", 'v'},
                {".--", 'w'},
                {"-..-", 'x'},
                {"-.--", 'y'},
                {"--..", 'z'},
                {"", ' '},
            };

            string result = "";

            foreach (string letter in morse_text.Split("/"))
            {
                if (dict.ContainsKey(letter))
                    result += dict[letter];
            }

            return result;
        }

        static void MatrixMultiplication()
        {
            static void PrintMatrix(int[,] m)
            {
                for (int i = 0; i < m.GetLength(0); i++)
                {
                    for (int j = 0; j < m.GetLength(1); j++)
                    {
                        Console.Write($"{m[i,j]}, ");
                    }
                    Console.Write("\n");
                }
            }
            
            int[,] a = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
            };

            int[,] b = new int[,]
            {
                { 8, 9 },
                { -5, 4 },
                { 10, -1 },
            };

            int[,] c = MultiplyMatrices(a, b);
            
            PrintMatrix(c);
        }

        /// <summary>
        /// 17. Násobení matic
        /// Čas: 13m40s
        /// </summary>
        /// <param name="a">Matice A</param>
        /// <param name="b">Matice B</param>
        /// <returns>Výsledná matice</returns>
        static int[,] MultiplyMatrices(int[,] a, int[,] b)
        {
            // Definujeme si rozměry matic
            int n = a.GetLength(0);
            int m = a.GetLength(1);
            int p = b.GetLength(1);
            
            // Výsledná matice
            int[,] c = new int[n,p];

            // Procházíme každý řádek v matici A
            for (int radekA = 0; radekA < n; radekA++)
            {
                // Procházíme každý sloupec v matici B
                for (int sloupecB = 0; sloupecB < p; sloupecB++)
                {
                    int item = 0;

                    // Spočítáme hodnotu prvku ve výsledné matici
                    for (int i = 0; i < m; i++)
                        item += a[radekA, i] * b[i, sloupecB];

                    c[radekA, sloupecB] = item;
                }                
            }

            return c;
        }

        static void MatrixDeterminant()
        {
            int[,] matrix = new int[,]
            {
                {5, 7, 6, 8},
                {1, 17, 4, 11},
                {3, 10, 6, 9},
                {6, 13, 1, 0},
            };
            
            Console.WriteLine(Determinant(matrix));
        }

        /// <summary>
        /// 18. Determinant matice
        /// Čas: 28m3s
        /// </summary>
        /// <param name="matrix">Matice, jejíž determinant chceme určit</param>
        /// <returns>Determinant matice</returns>
        /// <exception cref="ArgumentException">Pokud není matice v rozměru nxn</exception>
        static int Determinant(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                string message = "Matice není v rozměru nxn";
                Console.WriteLine(message);
                throw new ArgumentException(message);
            }

            int size = matrix.GetLength(0);

            // Spočítáme determinant pro různé rozměry matic
            switch (size)
            {
                case 1:
                    return matrix[0, 0];
                case 2:
                    return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
                case 3: // Sarrusovo pravidlo
                    return matrix[0,0] * matrix[1,1] * matrix[2,2] + matrix[0,1] * matrix[1,2] * matrix[2,0] + matrix[0,2] * matrix[1,0] * matrix[2,1]
                           - matrix[0,2] * matrix[1,1] * matrix[2,0] - matrix[0,0] * matrix[1,2] * matrix[2,1] - matrix[0,1] * matrix[1,0] * matrix[2,2];
                default: // Laplaceovo pravidlo
                    int det = 0;
                    for (int i = 0; i < size; i++)
                    {
                        // Vytvoří menší matici
                        int[,] smallerMatrix = new int[size - 1, size - 1];
                        for (int radek = 1; radek < size; radek++)
                        {
                            for (int sloupec = 0; sloupec < size; sloupec++)
                            {
                                if (sloupec != i)
                                {
                                    int smallSloupec = sloupec;
                                    if (smallSloupec > i)
                                        smallSloupec -= 1;
                                    smallerMatrix[radek-1, smallSloupec] = matrix[radek,sloupec];
                                }
                            }
                        }

                        // Přidáme správně vynásobený determinant redukované matice k determinantu původní matice
                        det += (int) Math.Pow(-1, i) * matrix[0,i] * Determinant(smallerMatrix);
                    }

                    return det;
            }
        }

        static void MatrixRotation()
        {
            static void PrintMatrix(int[,] matrix)
            {
                for (int radek = 0; radek < matrix.GetLength(0); radek++)
                {
                    for (int sloupec = 0; sloupec < matrix.GetLength(1); sloupec++)
                        Console.Write($"{matrix[radek, sloupec]}, ");
                    Console.Write("\n");
                }
            }
            
            int[,] matrix = new int[,]
            {
                { 5, 7, 6 },
                { 1, 17, 4 },
                { 3, 8, 6 },
                { 6, 13, 1 },
            };

            int[,] rotatedMatrix = RotateMatrix(matrix);

            PrintMatrix(rotatedMatrix);
        }

        /// <summary>
        /// 19. Rotace matice
        /// Čas: 18m6s
        /// </summary>
        /// <param name="matrix">Matice</param>
        /// <returns>Otočená matice</returns>
        static int[,] RotateMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            int[,] rotated = new int[columns, rows];
            
            // Překopíruje matrix s otočením o 90°
            for (int radek = 0; radek < rows; radek++)
            {
                for (int sloupec = 0; sloupec < columns; sloupec++)
                    rotated[sloupec, rows-1-radek] = matrix[radek, sloupec];
            }

            return rotated;
        }

        /// <summary>
        /// 20. Euklidův algoritmus
        /// Čas: 5m17s
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Největší společný dělitel čísel `a` a `b`</returns>
        static int Euclide(int a, int b)
        {
            while (b > 0)
            {
                int tmp = a % b;
                a = b;
                b = tmp;
            }

            return a;
        }
    }
}
