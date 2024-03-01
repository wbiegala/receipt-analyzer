![attention](./docs/images/attention-attracter.jpg)

# Czym jest ReceiptAnalyzer?

ReceiptAnalyzer to usługa web umożliwiająca analizę polskiego paragonu fiskalnego na podstawie jego zdjęcia lub skanu. Dane pozyskane w wyniku analizy mogą posłużyć do innych procesów realizowanych po stronie klienta (analiza wydatków, zgodność nagłówka/stopki ze standardami, wykrywanie nieprawidłowości, zbieranie danych statystycznych).

## Zakres pozyskanych danych
![receipt schema](./docs/images/receipt-schema.jpg)
+ Nagłówek paragonu:
	* nazwa sprzedawcy
	* adres sprzedawcy
	* nip sprzedawcy
+ Produkty: 
	* nazwa produktu
	* ilość
	* cena jednostkowa
	* cena całkowita
	* stawka VAT
	* udzielony rabat
	* inne (np. czy lek na receptę)
+ Podsumowanie: 
	* kwota całkowita
	* kwota udzielonych rabatów
	* kwoty poszczególnych stawek VAT
	* suma podatku VAT
+ Stopka: 
	* data i godzina transakcji
	* forma płatności
	* użycie karty lojalnościowej
	* numer paragonu w raporcie dobowym
	* numer unikatowy drukarki fiskalnej

## Cele badawcze
+ Azure Functions
+ Azure Service Bus
+ Azure Storage Account
+ Microservice architecture
+ Wstęp do Machine Learning (ML.NET)

## Tech stack
+ .NET 8 (ASP.NET, Entity Framework Core)
+ SQL Server
+ Azure Service Bus

## Roadmapa
1. Projekt
2. Model domeny
3. Model danych
4. WebAPI
5. Rozpoznawanie paragonów (zbiory danych, trenowanie modelu, testowanie modelu)
6. Rozpoznawanie elementów paragonu (zbiory danych, trenowanie modelu, testowanie modelu)
7. Konwersja obrazu na tekst 
8. Mapowanie danych
9. Wdrożenie

# Jak to działa?
## Uruchomienie
TODO

## Przypadki użycia
![use case diagram](./docs/diagrams/use-case-diagram.jpg)

## Diagram sekwencji (wysokopoziomowy)
![sequence diagram basic](./docs/diagrams/sequence-diagram-basic.jpg)

## Diagram komponetów
![component diagram](./docs/diagrams/component-diagram.jpg)