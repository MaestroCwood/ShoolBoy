using System.Collections.Generic; // ���������� ������������ ���� ��� ������������� List

[System.Serializable] // ��������� ������������� ������ ��� ����������
public class GameData
{
    // ������ ���������� ��������������� ������ ������
    public List<string> killedEnemies = new List<string>();
    public bool hasWeapon = false;
}
