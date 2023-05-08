# Zadanie. Multizbiór

* Krzysztof Molenda, ver. 01/2020.12.03

## Cel, zakres

Ćwiczenie w zakresie:
  * projektowania struktury danych
  * wykorzystania typów generycznych
  * implementacji i wykorzystania interfejsu `IEnumerable<T>`
  * wykorzystanie `Dictionary<TKey, TValue>`

## Sformułowanie problemu

Twoim zadaniem jest zaimplementowanie struktury danych `MultiSet<T>` opisującej _multizbiór_. Multizbiór jest rozszerzeniem koncepcji zbioru (w sensie matematycznym), umożliwia zapamiętanie duplikatów elementów.

Struktura ta przydaje się w wielu praktycznych sytuacjach. W bibliotekach C# nie została zrealizowana (a jest np. w STL/C++). Struktura o podobnych właściwościach, dostosowana do programowania współbieżnego, jest zaimplementowana w przestrzeni nazw `System.Collections.Concurrent`: [`ConcurrentBag<T>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1).

* Zrealizuj implementację multizbioru opakowując, obiekt typu `Dictionary<T, int>` funkcjonalnościami wskazanymi w interfejsie `IMultiSet<T>` dołączonemu do ćwiczenia. 

* Multizbiór jest `IEnumerable<T>`, elementy zwracane przez iterator mogą być nieuporządkowane (np. dla multizbioru `{a:3, c:2, b:1}` mogą być zwrócone jako `{a, a, a, c, c, b}`).

* Równość elementów multizbioru określona jest przez `Equals()` dla typu elementów - w wariancie tworzenia multizbioru konstruktorem bezparametrowym. Konstruktor multizbioru z parametrem `IEqualityComparer<T>` wymusza porówywanie elementów zdefiniowanym w obiekcie `comparer`.

* Ograniczaj ilość kodu, wykorzystuj łańcuchowanie kodu (dana funkcjonalność powinna być oprogramowana raz).

* Utwórz testy jednostkowe potwierdzające poprawność Twojej implementacji. Rozważ warianty dla typu `T`: 
    * wartościowego: `MultiSet<char>`
    * referencyjnego: `MultiSet<StringBuilder>`

Zapisy w `IMultiSet.cs` potraktuj jako wytyczne dla implementacji. Niektóre z metod są zakomentowane, ponieważ wymagane są przez inne interfejsy lub nie można ich zadeklarować w interfejsie (np. konstruktory, przeciążenia operatorów, metody statyczne, ...).

Funkcjonalność multizbioru jest zbieżna z funkcjonalnością zbioru  oraz słownika - zdefiniowanych w C#.

## Referencje
* <https://en.wikipedia.org/wiki/Multiset>
* <https://oeis.org/wiki/Multisets>
* [Interfejs `ISet`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1)
* [Interfejs `IDictionary<TKey, TValue>`](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)
