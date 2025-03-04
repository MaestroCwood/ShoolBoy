using System.Collections.Generic; // Подключаем пространство имен для использования List

[System.Serializable] // Позволяет сериализовать объект для сохранения
public class GameData
{
    // Список уникальных идентификаторов убитых врагов
    public List<string> killedEnemies = new List<string>();
    public bool hasWeapon = false;
}
