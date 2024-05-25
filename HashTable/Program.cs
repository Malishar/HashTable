using LibraryClass;

namespace HashTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyHashTable<BankCard> table = new MyHashTable<BankCard>();
            BankCard foundCard = null;
            int answer = 1;
            while (answer != 7)
            {
                try
                {
                    Console.WriteLine("1. Создать новую хэш-таблицу");
                    Console.WriteLine("2. Напечатать хэш-таблицу");
                    Console.WriteLine("3. Выполнить поиск элемента в хеш-таблице");
                    Console.WriteLine("4. Удалить найденный элемент из хеш-таблицы.");
                    Console.WriteLine("5. Добавить элемент в хеш-таблицу после введённого");
                    Console.WriteLine("6. Выйти");
                    answer = int.Parse(Console.ReadLine());
                    switch (answer)
                    {
                        case 1:
                            Console.WriteLine("Задайте размер хэш-таблицы: ");
                            int length = int.Parse(Console.ReadLine());
                            table = new MyHashTable<BankCard>(length);
                            for (int i = 0; i < length; i++)
                            {
                                BankCard card = new BankCard();
                                card.RandomInit();
                                table.AddPoint(card);
                            }
                            Console.WriteLine("Хэш-таблица создана и заполнена элементами");
                            break;
                        case 2:
                            table.PrintTable();
                            break;
                        case 3:
                            Console.WriteLine("Введите элемент для поиска: ");
                            BankCard searchCard = new BankCard();
                            searchCard.Init();
                            bool found = table.Contains(searchCard);
                            if (found == true)
                            {
                                foundCard = searchCard;
                                Console.WriteLine("Элемент найден");
                            }
                            else
                            {
                                Console.WriteLine("Элемент не найден");
                                foundCard = null;
                            }
                            break;
                        case 4:
                            if (foundCard == null)
                            {
                                Console.WriteLine("Сначала выполните поиск элемента");
                            }
                            else
                            {
                                bool removed = table.RemoveData(foundCard);
                                Console.WriteLine($"Элемент удален");
                                foundCard = null;
                            }
                            break;
                        case 5:
                            if (foundCard == null)
                            {
                                Console.WriteLine("Сначала выполните поиск элемента");
                            }
                            else
                            {
                                Console.WriteLine("Введите элемент для добавления: ");
                                BankCard newCard = new BankCard();
                                newCard.Init();
                                bool added = table.AddAfter(foundCard, newCard);
                                Console.WriteLine($"Элемент добавлен");
                            }
                            break;
                        case 6:
                            Console.WriteLine("Программа завершена");
                            break;
                        default:
                            Console.WriteLine("Некорректный ввод. Попробуйте еще раз");
                            break;
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
            }
        }
    }
}