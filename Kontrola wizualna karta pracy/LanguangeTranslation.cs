using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class LanguangeTranslation
    {
        public static string Translate(string input, bool langPolish)
        {
            string result = "";

            if (langPolish)
            {
                switch (input)
                {
                    case "BrakLutowia": return "Wady lutowia";
                    case "BrakDiodyLed": return "Brak LED/polar/podniesiona";
                    case "BrakResConn": return "Brak RESS/CONN";
                    case "PrzesuniecieLed": return "Przesunięcie LED";
                    case "PrzesuniecieResConn": return "Przesunięcie RES/CONN";
                    case "ZabrudzenieLed": return "Zabrudzenie LED";
                    case "UszkodzenieMechaniczneLed": return "Uszk. mechaniczne LED";
                    case "UszkodzenieConn": return "Uszk. mechaniczne CONN";
                    case "WadaFabrycznaDiody": return "Wada fabryczna LED";
                    case "UszkodzonePcb": return "Uszkodzenie PCB";
                    case "WadaNaklejki": return "Wada naklejki";
                    case "SpalonyConn": return "Spalony CONN";
                    case "Inne": return "Inne";
                    case "TestElektryczny": return "Test elektryczny";
                    default: return input;
                }
            }
            else
            {
                switch (input)
                {
                    case "Ten numer PCB należy do innego zlecenia!": return "этот продукт принадлежит другому LOT";
                    case "BrakLutowia": return "Дефекты припоя";
                    case "BrakDiodyLed": return "Нет/поляризации/повышен LED";
                    case "BrakResConn": return "Нет RES/CONN";
                    case "PrzesuniecieLed": return "Cдвинутый LED";
                    case "PrzesuniecieResConn": return "Cдвинутый RES/CONN";
                    case "ZabrudzenieLed": return "Грязный LED";
                    case "UszkodzenieMechaniczneLed": return "LED механические повреждения";
                    case "UszkodzenieConn": return "CONN механические повреждения";
                    case "WadaFabrycznaDiody": return "Фабричный дефект LED";
                    case "UszkodzonePcb": return "повреждение PCB";
                    case "WadaNaklejki": return "Дефект наклейки";
                    case "SpalonyConn": return "Cожжен CONN";
                    case "Inne": return "Другой";
                    case "TestElektryczny": return "Электрический тест";
                    case "Ilość dobrych": return "Количество хорошых";
                    case "Zapisz": return "Сохранить";
                    case "Ilość od początku zmiany:": return "Количество с начала смены:";
                    case "szt": return "штук";
                    case "Dane zlecenia": return "Данные заказа";
                    case "Nieprawidłowy numer zlecenia": return "Недопустимый номер заказа";
                    case "Nieprawidłowa nazwa operatora": return "Недопустимое имя оператора";
                    case "Nieznany": return "неизвестный";
                    case "Model": return "Модель";
                    case "Produkcja": return "Производство";
                    case "Ilość złączek": return "Количество CONN";
                    case "Model: Nieznany": return "Модель: Неизвестный";
                    case "Najpierw wpisz numer zlecenia": return "Сначала введите номер заказа";
                    case "szt/godz": return "ед/час";
                    case "Brak danych o testach": return "Нет данных испытаний";
                    case "Wyniki testu": return "Результаты тестов";
                    case "Numer zlecenia": return "Номер заказа";
                    case "Operator": return "Oператор";
                    case "Dodaj wadę": return "Добавить дефект";
                    case "ODPAD": return "ОТХОДЫ";
                    case "ostatnie 8h:": return "последние 8 часов:";
                    case "Odpad TOP 5 przyczyn odpadu:": return "Отходы Топ-5 причины отходов:";
                    case "WYDAJNOŚĆ": return "ЭФФЕКТИВНОСТЬ";
                    case "Norma: 2500 szt./ zm.": return "Стандарт: 2500 шт./см.";
                    case "312 szt/godz": return "312 шт./час";
                    case "Aktualnie średnio": return "настоящее, в среднем";
                    case "Zeskanuj kod Qr": return "сканировать код QR";
                    case "+zdjęcie": return "+картина";
                    case "Max 10 zdjęć.Skasuj zdjęcia aby dodać nowe.": return "Макс. 10 фотографий. Удалите фотографии, чтобы добавить новые.";
                    case "Wbierz przyczynę wady": return "Выберите причину дефекта";
                    case "Dodaj zdjęcia wad": return "Добавить фотографии дефектов";
                    case "Nie można odczytać kodu, wpisz ręcznie:": return "Код не может быть прочитан, введите вручную:";
                    case "SPRAWDŹ POPRAWNOŚĆ DANYCH:": return "ПРОВЕРЬТЕ ДАННЫЕ ПРАВИЛЬНО:";
                    case "Nieprawidłowa ilość": return "Неверное количество";
                    case "Uruchom ponownie aplikację aby zmienić kamerę": return "Перезапустить приложение для смены камеры";
                    case "Zamknąć program?": return "Закрыть программу?";
                    case "Zakończenie pracy": return "Конец работы";
                    case "TAK": return "ДА";
                    case "NIE": return "НЕТ";

                } 
            }
            return result;
        }

    }
}
