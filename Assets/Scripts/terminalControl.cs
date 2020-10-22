using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminalControl : MonoBehaviour
{
	enum Screen { MainMenu, Password, Win }
	Screen currentScreen = Screen.MainMenu;
	const string menuHint = "Напишите 'Меню', чтобы вернуться назад";
	//Выбранный уровень
	int level;

	string password;

	string[] Level1Passwords = { "ключ", "книга", "шкаф", "ручка", "текст" };
	string[] Level2Passwords = { "дубинка", "арест", "закон", "рапорт", "начальник" };
	string[] Level3Passwords = { "спутник", "комета", "орион", "интерстеллар", "космонавт" };

	void Start()
	{
		ShowMainMenu("юзер");
	}

	void ShowMainMenu(string playerName)
	{
		currentScreen = Screen.MainMenu;
		Terminal.ClearScreen();
		Terminal.WriteLine("Привет" + playerName + "!");
		Terminal.WriteLine("Какой терминал вы хотите взломать сегодня?");
		Terminal.WriteLine(" ");
		Terminal.WriteLine("Введите 1 - городская библиотека");
		Terminal.WriteLine("Введите 2 - полицейский участок");
		Terminal.WriteLine("Введите 3 - космический корабль");
		Terminal.WriteLine("Ваш выбор: ");
	}
	void OnUserInput(string input)
	{
		if (input == "Меню")
		{
			ShowMainMenu(", рад видеть тебя снова");
		}
		else if (currentScreen == Screen.MainMenu)
		{
			RunMainMenu(input);
		}
		else if (currentScreen == Screen.Password)
		{
			CheckPassword(input);
		}
	}

	void RunMainMenu(string input)
	{
		bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");

		if (isValidLevelNumber)
		{
			level = int.Parse(input);
			GameStart();
		}
		else if (input == "король")
		{
			Terminal.WriteLine("Кланяюсь, ваше величество!");
		}
		else
		{
			Terminal.WriteLine("Введите правильное значение...");
		}
	}
	void GameStart()
	{
		Terminal.ClearScreen();
		currentScreen = Screen.Password;

		switch (level)
		{
			case 1:
				password = Level1Passwords[Random.Range(0, Level1Passwords.Length)];
				Terminal.WriteLine("Вы в городской библиотеке.");
				break;
			case 2:
				password = Level2Passwords[Random.Range(0, Level2Passwords.Length)];
				Terminal.WriteLine("Вы в полицейском участке.");
				break;
			case 3:
				password = Level3Passwords[Random.Range(0, Level3Passwords.Length)];
				Terminal.WriteLine("Вы на космическом корабле.");
				break;
			default:
				Debug.LogError("Такого уровня не существует!");
				break;
		}
		Terminal.WriteLine("Подсказка: " + password.Anagram());
		Terminal.WriteLine(menuHint);
		Terminal.WriteLine("Введите пароль:");
	}
	void CheckPassword(string input)
	{
		if (input == password)
		{
			ShowWinScreen();
		}
		else
		{
			GameStart();
		}
	}

	void ShowWinScreen()
	{
		Terminal.ClearScreen();
		Reward();
	}

	void Reward()
	{
		currentScreen = Screen.Win;
		switch (level)
		{
			case 1:
				Terminal.WriteLine("Пароль верный! Держите вашу книгу:");
				Terminal.WriteLine(
					@"
    _______
   /      /,
  /      //
 /______//
(______(/
					");
				break;
			case 2:
				Terminal.WriteLine("Пароль верный! Вот ваш пистолет:");
				Terminal.WriteLine(
					@"
  	  __,_____
	 / __.==--)
    /#(-'
    `-'
					");
				break;
			case 3:
				Terminal.WriteLine("Пароль верный! Вот ваш космолет:");
				Terminal.WriteLine(
					@"
    /\
   /  \
  |STAR|
  |    |
 '      `
 |      |
 |______|
  '-`'-` 
					");
				break;
		}
		Terminal.WriteLine(menuHint);
	}
}