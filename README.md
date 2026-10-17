# ShoolBoy

A 3D Unity game about moving through a school, fighting enemies, and using weapons and interactive objects. The scripts I wrote are in `Assets/Scripts`. The project also contains asset-store content that I did not write.

## What the scripts cover

- player: `PlayerMeneger`, `PlayerIneractiveController`, `PlayerUIControl`, `PlayerAudioControl`
- weapons: `Weapon`, `WeaponThrowAndPickUp`, `Bullet`, `AddBullet`
- enemies: `ZombieController`, `SpawnEnemy`, `PoliceControl`, `EnemyFXhit`
- interactions: `Safe`, `ParolSafe`, `LastKeyControl`, `SetUpDinamite`, `PhoneBothConroller`
- progress: `SaveSystem` stores the names of defeated enemies

## Save

`SaveSystem` keeps a `GameData` object with `List<string> killedEnemies`. `SaveProgress` turns that object into JSON with `JsonUtility.ToJson` and writes it to `PlayerPrefs` under the key `KilledEnemies`. `LoadProgress` reads the same key back. `ResetProgress` deletes the key. There is no database and no cloud save in this class.

`YandexGetPlatform` and `ReklamFullScrin` are the hooks toward the Yandex Games platform and a fullscreen ad. They are not a shop.

## Limitations

The script names keep their original spelling. This repository is a game project, not an infrastructure project. I did not find screenshots of the running game that I can publish without using illustrated promo art.

Screenshots still worth adding later:

- a view of the school level
- the player with a weapon
- the on-screen interface
- a safe, key, or other interactive object
