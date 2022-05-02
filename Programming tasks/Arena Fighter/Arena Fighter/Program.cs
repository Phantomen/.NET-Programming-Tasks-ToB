// See https://aka.ms/new-console-template for more information
using System;

class Battle
{
    static Random rnd = new Random();
    static int healthMax = 30;
    static int healthMin = 10;
    static int strengthMax = 5;
    static int speedMax = 5;

    static int scoreRetire = 50;
    static int scoreWin = 150;
    static int coinsWin = 3;

    static List<Character> playerFighterList = new List<Character>();

    static void Main(string[] args)
    {
        Arena[] arenas = new Arena[3];
        arenas[0] = new Arena("Ice",
            Weapon.WeaponType.Piercing, 1, Weapon.WeaponType.Bludgeoning, -1,
            Armor.ArmorType.Heavy, 1, Armor.ArmorType.Light, -1);
        arenas[1] = new Arena("Desert", Weapon.WeaponType.Slashing, 1, Weapon.WeaponType.Piercing, -1,
            Armor.ArmorType.Light, 1, Armor.ArmorType.Heavy, -1);
        arenas[2] = new Arena("Forest", Weapon.WeaponType.Bludgeoning, 1, Weapon.WeaponType.Slashing, -1,
            Armor.ArmorType.Medium, 0, Armor.ArmorType.Medium, 0);

        while (true)
        {
            //Outfit your fighter
            Console.WriteLine("Please name your fighter");
            string fighterName = Console.ReadLine();

            Console.WriteLine("\nWhat does your fighter fight with?\n" +
                "1 - Sword\n" +
                "2 - Spear\n" +
                "3 - Mace");
            Weapon weaponToFight = new Weapon((Weapon.WeaponType)GetValidInt(1, 3));

            Console.WriteLine("\nWhat does your fighter wear as armor?\n" +
                "1 - Light armor\n" +
                "2 - Medium armor\n" +
                "3 - Heavy armor");
            Armor armorToFight = new Armor((Armor.ArmorType)GetValidInt(1, 3));

            Character playerFighter = new Character(fighterName, rnd.Next(healthMax - healthMin) + healthMin, rnd.Next(strengthMax) + 1, rnd.Next(speedMax) + 1, weaponToFight, armorToFight);



            int fightsSurvived = 0;
            bool retired = false;
            bool dead = false;

            //While fighter is not dead or retired, continue fighting with fighter
            while (!retired && !dead)
            {
                //Create enemy fighter
                Character enemyFighter = new Character("Opponent " + (fightsSurvived + 1).ToString(), rnd.Next(healthMax - healthMin) + healthMin, rnd.Next(strengthMax) + 1, rnd.Next(speedMax) + 1,
                    new Weapon((Weapon.WeaponType)rnd.Next(Enum.GetNames(typeof(Weapon.WeaponType)).Length)), new Armor((Armor.ArmorType)rnd.Next(Enum.GetNames(typeof(Armor.ArmorType)).Length)));
                List<Round> roundList = new List<Round>();

                int currentRound = 0;

                int currentArena = rnd.Next(arenas.Length);

                Console.WriteLine("\n\n\nThe fighters will fight in the {0} arena!!!\n", arenas[currentArena].arenaName);

                //While both fighters are alive, fight
                while (playerFighter.IsAlive() && enemyFighter.IsAlive())
                {
                    currentRound++;
                    Round round = new Round();
                    Console.WriteLine("\nRound: " + currentRound.ToString());
                    round.Fight(arenas[currentArena], playerFighter, enemyFighter);
                    roundList.Add(round);
                }

                //Add the rounds to the player fighter
                playerFighter.AddRound(roundList);

                if (playerFighter.IsAlive())
                {
                    fightsSurvived++;
                    playerFighter.AddScore(scoreWin);
                    Console.WriteLine("\n0 - Do you want to retire \n"+ 
                        "1 - Continue fighting?\n");

                    int retireInt = GetValidInt(0, 1);
                    if (retireInt == 0)
                    {
                        playerFighter.AddScore(scoreRetire);
                        Console.WriteLine("You retired {0} after having won {1} rounds and accumulated a total score of: {2}", playerFighter.GetName(), playerFighter.GetBattlesWon(), playerFighter.GetScore());
                        retired = true;
                    }

                    else
                    {
                        playerFighter.AddCoins(coinsWin);

                        Console.WriteLine("You have {0} coins, do you want to go to the shop?\n" +
                            "0 - No\n" +
                            "1 - Yes", playerFighter.GetCoins());

                        int buyAtStore = GetValidInt(0, 1);
                        if(buyAtStore == 1)
                        {
                            Shop(playerFighter);
                        }

                        playerFighter.Heal();
                    }
                }

                else
                {
                    dead = true;
                    Console.WriteLine("\nSadly " + playerFighter.GetName() + " died fighting.");
                    Console.WriteLine("{0} died after having won {1} rounds and accumulated a total score of: {2}", playerFighter.GetName(), playerFighter.GetBattlesWon() - 1, playerFighter.GetScore());
                }
            }

            //Add the player to the list as they are either dead or retired
            playerFighterList.Add(playerFighter);

            bool looping = true;

            //Do you want to rewatch previous battles, make a new fighter or exit?
            while (looping)
            {
                Console.WriteLine("\nPress 1 to play again with a new fighter \n" +
                    "Press 2 to rewatch the battles for a figher \n" +
                    "Press 0 to exit \n");
                int exitInt = GetValidInt(0, 2);
                if (exitInt == 0) { ExitProgram(); }
                else if (exitInt == 1) { looping = false; }
                else if (exitInt == 2)
                {
                    Console.WriteLine("Please select a fighter!");
                    int numberOfFighters = playerFighterList.Count;
                    for (int i = 0; i < numberOfFighters; i++)
                    {
                        Console.WriteLine("{0} - {1}, {2}, score: {3}", i, playerFighterList[i].GetName(), playerFighterList[i].IsAlive() ? "Retired" : "Dead", playerFighterList[i].GetScore());
                    }
                    int fighterInt = exitInt = GetValidInt(0, numberOfFighters - 1);

                    playerFighterList[fighterInt].ReadRounds();
                }

                Console.Clear();
            }
        }
    }

    static void ExitProgram()
    {
        Environment.Exit(0);
    }

    //Returns a valid int between two values
    static int GetValidInt(int min, int max)
    {
        int intToGet = 0;
        bool valid = false;
        while (!valid)
        {
            valid = int.TryParse(Console.ReadLine(), out intToGet);
            if(intToGet < min || intToGet > max) { valid = false; }
            if (!valid)
            {
                Console.WriteLine("Please input a valid int between {0} and {1}", min, max);
            }
        }

        return intToGet;
    }



    static int costIncrease = 2;
    static int costStandard = 3;
    static int damageIncrease = 1;
    static int hpIncrease = 2;
    static void Shop(Character fighter)
    {
        Console.Clear();
        bool buying = true;

        //you can continue to buy as long as you don't exit the shop
        while (buying)
        {
            int weaponUpgradeCost = costStandard + (costIncrease * fighter.wieldedWeapon.GetCurrentUpgrade());
            int armorUpgradeCost = costStandard + (costIncrease * fighter.wornArmor.GetUpgradeLevel());
            Console.WriteLine("Do you want to upgrade your weapon or armor? You have {0} coins\n" +
                "0 -  No\n" +
                "1 - Weapon, Costs {1}\n" +
                "2 - Armor, Costs {2}\n", fighter.GetCoins(), weaponUpgradeCost, armorUpgradeCost);

            int valid = GetValidInt(0, 2);

            //upgrade weapon
            if (valid == 1)
            {
                if(fighter.GetCoins() >= weaponUpgradeCost)
                {
                    fighter.AddCoins(-weaponUpgradeCost);
                    fighter.wieldedWeapon.UpgradeWeapon(damageIncrease);
                    Console.WriteLine("Your weapon has been upgraded\n");
                }
                else { Console.WriteLine("You do not have enough coins to upgrade your weapon"); }
            }

            //upgrade armor
            else if (valid == 2)
            {
                if (fighter.GetCoins() >= armorUpgradeCost)
                {
                    fighter.AddCoins(-armorUpgradeCost);
                    fighter.UpgradeArmor(hpIncrease);
                    Console.WriteLine("Your armor has been upgraded\n");
                }
                else { Console.WriteLine("You do not have enough coins to upgrade your weapon"); }
            }

            else { buying = false; }
        }
    }
}

class Round
{
    Random rnd = new Random();

    string roundString = "";

    public string Fight(Arena arena, Character fighter, Character opponent)
    {
        //Which fighter is faster?
        int fSpeed = fighter.GetSpeed() + RollDice();
        int oSpeed = opponent.GetSpeed() + RollDice();

        roundString = "";

        //Faster attacker attacks forst
        if (fSpeed > oSpeed)
        {
            roundString += Attack(arena, fighter, opponent);
        }

        else if (fSpeed < oSpeed)
        {
            roundString += Attack(arena, opponent, fighter);
        }

        //If both have the same speed, both attack eachother at the same time
        else
        {
            //Roll the dice, calculate the damage and apply it
            int rolledDmg1 = RollDice();
            int rolledDmg2 = RollDice();
            int dmg1 = fighter.DealDamage() + rolledDmg1 + arena.WeaponInArena(fighter.wieldedWeapon.GetWeaponType()) - arena.ArmorInArena(opponent.wornArmor.GetArmor());
            int dmg2 = opponent.DealDamage() + rolledDmg2 + arena.WeaponInArena(opponent.wieldedWeapon.GetWeaponType()) - arena.ArmorInArena(fighter.wornArmor.GetArmor());
            opponent.TakeDamage(dmg1);
            fighter.TakeDamage(dmg2);

            string tempString = "Both fighters hit eachother at the same time! With " + fighter.GetName() + " taking " + dmg2 + " (" + rolledDmg1 + ") damage (" + fighter.GetCurrentHealth() + ")" +
                " and " + opponent.GetName() + " taking " + dmg1 + " (" + rolledDmg2 + ") damage! (" + opponent.GetCurrentHealth() + ")";

            roundString += tempString;

            Console.WriteLine(tempString);

            //Did someone die?
            if(fighter.IsAlive() && opponent.IsAlive())
            {
                roundString += "\n and they are both still standing!";
            }

            else if(fighter.IsAlive() && !opponent.IsAlive())
            {
                roundString += "\n" + opponent.GetName() + " goes down from the mutual exchange!";
            }

            else if (!fighter.IsAlive() && opponent.IsAlive())
            {
                roundString += "\n" + fighter.GetName() + " goes down from the mutual exchange!";
            }

            else if (!fighter.IsAlive() && !opponent.IsAlive())
            {
                roundString += "\nUnbelivable! " + fighter.GetName() + " and " + opponent.GetName() + " both goes down!";
            }
        }

        return roundString;
    }

    string Attack(Arena arena, Character attacker, Character defender)
    {
        //Roll the dice and calculate the damage for the faster attacker adn then apply the damage
        int rolledDmg = RollDice();
        int damage = attacker.DealDamage() + rolledDmg + arena.WeaponInArena(attacker.wieldedWeapon.GetWeaponType()) - arena.ArmorInArena(defender.wornArmor.GetArmor());
        defender.TakeDamage(damage);
        string tempString = attacker.GetName() + " is faster and attacks, dealing " + damage + " (" + rolledDmg + ") damage! (" + defender.GetCurrentHealth() + ")\n";
        string s = tempString;
        Console.WriteLine(tempString);

        //If the defender is still alive from the attack, it is their turn to attack
        if (defender.IsAlive())
        {
            rolledDmg = RollDice();
            damage = defender.DealDamage() + rolledDmg + arena.WeaponInArena(defender.wieldedWeapon.GetWeaponType()) - arena.ArmorInArena(attacker.wornArmor.GetArmor());
            attacker.TakeDamage(damage);

            tempString = defender.GetName() + " is still standing and he goes in for his attack, dealing " + damage + " (" + rolledDmg + ") damage! (" + attacker.GetCurrentHealth() + ")";
            s += tempString;
            Console.WriteLine(tempString);

            if (!attacker.IsAlive())
            {
                tempString = "\nAnd " + attacker.GetName() + " goes down from the counter attack!";
                s += tempString;
                Console.WriteLine(tempString);
            }
        }

        else
        {
            tempString = "And " + defender.GetName() + " goes down!";
            s += tempString;
            Console.WriteLine(tempString);
        }

        return s;
    }

    public int RollDice() { return rnd.Next(6) + 1; }

    public string GetRound()
    {
        return roundString;
    }
}

class Arena
{
    public string arenaName { get; }
    Weapon.WeaponType arenaWeaponBonusType;
    int arenaWeaponBonusInt;
    Weapon.WeaponType arenaWeaponPenaltyType;
    int arenaWeaponPenaltyInt;

    Armor.ArmorType arenaArmorBonusType;
    int arenaArmorBonusInt;
    Armor.ArmorType arenaArmorPenaltyType;
    int arenaArmorPenaltyInt;

    public Arena(string nm,
        Weapon.WeaponType weaponBonusType, int weaponBonusInt, Weapon.WeaponType weaponPenaltyType, int weaponPenaltyInt,
        Armor.ArmorType armorBonusType, int armorBonusInt, Armor.ArmorType armorPenaltyType, int armorPenaltyInt)
    {
        arenaName = nm;
        arenaWeaponBonusType = weaponBonusType;
        arenaWeaponBonusInt = weaponBonusInt;
        arenaWeaponPenaltyType = weaponPenaltyType;
        arenaWeaponPenaltyInt = weaponPenaltyInt;
        arenaArmorBonusType = armorBonusType;
        arenaArmorBonusInt = armorBonusInt;
        arenaArmorPenaltyType = armorPenaltyType;
        arenaArmorPenaltyInt = armorPenaltyInt;

    }

    public int WeaponInArena(Weapon.WeaponType fightingWeapon)
    {
        if (fightingWeapon == arenaWeaponBonusType) { return arenaWeaponBonusInt; }
        else if (fightingWeapon == arenaWeaponPenaltyType) { return arenaWeaponPenaltyInt; }

        return 0;
    }

    public int ArmorInArena(Armor.ArmorType armorType)
    {
        if(armorType == arenaArmorBonusType) { return arenaArmorBonusInt; }

        else if (armorType == arenaArmorPenaltyType) { return arenaArmorPenaltyInt; }

        return 0;
    }
}

class Armor
{
    public enum ArmorType { Light, Medium, Heavy }
    ArmorType armorType;

    int lightHpBonus = 2;
    int lightSpeedBonus = 2;
    int mediumHpBonus = 4;
    int mediumSpeedBonus = 0;
    int heavyHpBonus = 8;
    int heavySpeedBonus = -4;

    int upgradeLevel = 0;
    int armorSpeed = 0;

    public Armor(ArmorType at = ArmorType.Light)
    {
        armorType = at;

        switch (armorType)
        {
            case ArmorType.Light:
                armorSpeed = lightSpeedBonus;
                break;

            case ArmorType.Medium:
                armorSpeed = mediumSpeedBonus;
                break;

            case (ArmorType.Heavy):
                armorSpeed = heavySpeedBonus;
                break;

            default:
                armorSpeed = 0;
                break;
        }
    }

    public ArmorType GetArmor() { return armorType; }

    public int GetHpBonus()
    {
        switch (armorType)
        {
            case ArmorType.Light:
                return lightHpBonus * (upgradeLevel + 1);

            case ArmorType.Medium:
                return mediumHpBonus * (upgradeLevel + 1);

            case (ArmorType.Heavy):
                return heavyHpBonus * (upgradeLevel + 1);

            default:
                return 0;
        }
    }

    public int GetSpeedBonus()
    {
        switch (armorType)
        {
            case ArmorType.Light:
                return lightSpeedBonus;

            case ArmorType.Medium:
                return mediumSpeedBonus;

            case (ArmorType.Heavy):
                return heavySpeedBonus;

            default:
                return 0;
        }
    }

    public int UpgradeArmor(int hpIncrease)
    {
        upgradeLevel++;

        switch (armorType)
        {
            case ArmorType.Light:
                armorSpeed++;
                lightHpBonus += hpIncrease;
                return hpIncrease;

            case ArmorType.Medium:
                hpIncrease += 1;
                mediumHpBonus += hpIncrease;
                return hpIncrease;

            case (ArmorType.Heavy):
                armorSpeed--;
                hpIncrease += 2;
                heavyHpBonus += hpIncrease;
                return hpIncrease;

            default:
                return hpIncrease;
        }
    }

    public int GetUpgradeLevel()
    {
        return upgradeLevel;
    }
}

class Weapon
{
    public enum WeaponType { Slashing, Piercing, Bludgeoning }
    WeaponType weaponType;
    int weaponDamage = 0;
    int weaponUpgrade = 0;

    public Weapon(WeaponType wt, int wd = 0)
    {
        weaponType = wt;
        weaponDamage = wd;
    }

    public WeaponType GetWeaponType() { return weaponType; }

    public void UpgradeWeapon(int wdu)
    {
        weaponDamage += wdu;
        weaponUpgrade++;
    }

    public int GetCurrentUpgrade() { return weaponUpgrade; }

    public int GetWeaponDamage() { return weaponDamage; }
}



class Character
{
    string name;
    int maxHealth;
    int currentHealth;
    int strength;
    int speed;
    int score = 0;
    int coins = 0;

    public Weapon wieldedWeapon { get; private set; }
    public Armor wornArmor { get; private set; }

    List<List<Round>> roundList = new List<List<Round>>();
    

    public Character(string n, int h, int str, int spd, Weapon w, Armor a)
    {
        name = n;
        maxHealth = h + a.GetHpBonus();
        currentHealth = maxHealth;
        strength = str;
        speed = spd;
        wieldedWeapon = w;
        wornArmor = a;
    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public bool IsAlive()
    {
        if(currentHealth > 0)
        {
            return true;
        }

        return false;
    }

    public int DealDamage()
    {
        return strength + wieldedWeapon.GetWeaponDamage();
    }

    public string[] GetFighterStats()
    {
        string[] stats = { name, maxHealth.ToString(), currentHealth.ToString() };
        return stats;
    }

    public int GetSpeed() { return speed + wornArmor.GetSpeedBonus(); }
    public string GetName() { return name; }
    public int GetCurrentHealth() { return currentHealth; }

    public void AddRound(List<Round> rounds)
    {
        roundList.Add(rounds);
    }

    public int GetBattlesWon()
    {
        return roundList.Count;
    }


    //read the previous fights from the previous fights
    public void ReadRounds()
    {
        bool reading = true;
        while (reading)
        {
            Console.WriteLine("\n{0} has been part of {1} battles, which of these would you like to see? Input a value from 1 to {1}" +
                "\nOr enter 0 to exit back to the arena", name, roundList.Count);
            int battleInt = 0;
            bool valid = false;
            while (!valid)
            {
                valid = int.TryParse(Console.ReadLine(), out battleInt);
                if (battleInt == 0) { return; }
                if (battleInt < 0 || battleInt > roundList.Count) { valid = false; }
                if (!valid)
                {
                    Console.WriteLine("Please input a valid int between 0 and {1}\n", roundList.Count);
                }
            }

            if(battleInt == 0) { return; }

            battleInt--;
            for (int i = 0; i < roundList[battleInt].Count; i++)
            {
                Console.WriteLine(roundList[battleInt][i].GetRound());
            }
        }
    }

    public void AddScore(int sc)
    {
        score += sc;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCoins() { return coins; }
    public void AddCoins(int c) { coins += c; }

    public void UpgradeArmor(int hpIncrease)
    {
        wornArmor.UpgradeArmor(hpIncrease);
        maxHealth += wornArmor.UpgradeArmor(hpIncrease);
    }
}