namespace OurGame
{
    // для событий в объектах для оповещения Game
    delegate void Message();

    // для вывода сообщений в журнал
    delegate void SendMessage(string msg);
}