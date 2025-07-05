using System;
using System.Linq;
using System.Threading.Tasks;
using TL;
using WTelegram;

namespace TelegramGroupScanner
{
    class Program
    {
        private static Client _client;
        private static string _phoneNumber;

        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== Telegram Group Scanner ===");
                Console.WriteLine("Введите номер телефона Telegram (+79XXXXXXXXX):");
                _phoneNumber = Console.ReadLine();

                // Создаем клиента с помощью нашего метода Config
                _client = new Client(Config);
                // Выполняем логин (если требуется, появится запрос на код)
                var me = await _client.LoginUserIfNeeded();
                Console.WriteLine($"✅ Вход выполнен успешно как {me.username}");

                // Получаем все чаты (группы, личные сообщения, каналы)
                var chatsResult = await _client.Messages_GetAllChats();
                // Отбираем только чаты, которые являются группами (не каналами)
                var groups = chatsResult.chats.Values
                    .Where(chat => chat is ChatBase && !((ChatBase)chat).IsChannel)
                    .Cast<ChatBase>()
                    .ToList();

                Console.WriteLine($"\nНайдено {groups.Count} групп:");
                foreach (var group in groups)
                    Console.WriteLine($"- {group.Title} (ID: {group.ID})");

                // Текст, который будет отправляться участникам
                string templateMessage = "Привет! Ваш номер был найден в группе.";

                // Обрабатываем каждую группу
                foreach (var group in groups)
                {
                    Console.WriteLine($"\n🔍 Анализ группы: {group.Title}");
                    await AnalyzeGroupMessages(group, templateMessage, chatsResult.users);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
                _client.Dispose();
            }
        }

        // Метод для передачи конфигурационных параметров (номер телефона, код, имя и т.д.)
        private static string Config(string what)
        {
            switch (what)
            {
                case "phone_number": return _phoneNumber;
                case "verification_code":
                    Console.WriteLine("Введите код подтверждения из Telegram:");
                    return Console.ReadLine();
                case "first_name": return "User";
                case "last_name": return "Scanner";
                case "password":
                    Console.WriteLine("Введите пароль двухфакторной аутентификации (если есть):");
                    return Console.ReadLine();
                default: return null;
            }
        }

        // Анализ сообщений в группе: ищем сообщения с телефонными номерами и отправляем ответ
        private static async Task AnalyzeGroupMessages(ChatBase group, string templateMessage, System.Collections.Generic.Dictionary<long, User> usersDict)
        {
            try
            {
                // Получаем историю сообщений группы
                var history = await _client.Messages_GetHistory(group);
                // Фильтруем только текстовые сообщения
                var messages = history.Messages.OfType<Message>();

                foreach (var msg in messages)
                {
                    // Если текст пустой — пропускаем
                    if (string.IsNullOrEmpty(msg.message))
                        continue;

                    // Ищем в сообщении номер телефона с использованием регулярного выражения
                    var phone = ExtractPhoneNumber(msg.message);
                    if (string.IsNullOrEmpty(phone))
                        continue;

                    // Получение отправителя: если msg.From (тип TL.Peer) является PeerUser
                    // Используем поле from_id, которое должно быть типа PeerUser
                    if (msg.from_id is PeerUser peerUser)
                    {
                        // Попытка получить объект TL.User из словаря пользователей, полученного вместе с историей
                        if (usersDict.TryGetValue(peerUser.user_id, out User fromUser))
                        {
                            Console.WriteLine($"- Найдено сообщение от {fromUser.username}: {msg.message.Trim()}");
                            // Отправка личного сообщения отправителю
                            await SendPrivateMessage(fromUser, $"Привет! {templateMessage} Группа: {group.Title}");
                            // Пауза для предотвращения возможного блокирования
                            await Task.Delay(2000);
                        }
                        else
                        {
                            Console.WriteLine("- Не удалось найти данные пользователя по user_id " + peerUser.user_id);
                        }
                    }
                    else
                    {
                        Console.WriteLine("- Отправитель не является пользователем (вероятно, системное сообщение).");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Ошибка в группе {group.Title}: {ex.Message}");
            }
        }

        // Простой метод для извлечения номера телефона из текста
        private static string ExtractPhoneNumber(string text)
        {
            // Регулярное выражение для русского формата номеров: +7 (XXX) XXX-XX-XX, 8XXXXXXXXXX и подобные
            var regex = new System.Text.RegularExpressions.Regex(@"(\+7|8)[\s\-]?\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2}");
            var match = regex.Match(text);
            return match.Success ? match.Value : null;
        }

        // Отправка личного сообщения по объекту TL.User
        private static async Task SendPrivateMessage(User user, string message)
        {
            try
            {
                // В новых версиях WTelegramClient можно напрямую передавать объект User
                await _client.SendMessageAsync(user, message);
                Console.WriteLine($"✉️ Сообщение отправлено пользователю {user.username}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Не удалось отправить сообщение пользователю {user.username}: {ex.Message}");
            }
        }
    }
}
