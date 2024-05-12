class Character:
    def __init__(self, name, health, defence, attack_power):
        self.name = name
        self.base_health = health

        self.health = health
        self.defence = defence
        self.attack_power = attack_power

    def attack(self, target: "Character"):
        self.attack_with_power(target, self.attack_power)

    def attack_with_power(self, target: "Character", attack_power: int = None):
        print(f"{self.name} ={attack_power}=> {target.name}")
        target.protect(attack_power)

    def protect(self, damage: int):
        defence_value = self.get_defence
        effective_damage = damage * (1 - (defence_value / 100))
        self.health -= effective_damage

        print(f"{self.name} █{defence_value}█ >> {effective_damage}. {self.health} HP")
        if self.health <= 0:
            self.die()

    def die(self):
        print(f"Персонаж {self.name} умер")

    @property
    def health_percentage(self) -> int:
        return (self.health / self.base_health) * 100
    
    @property
    def get_defence(self) -> int:
        return self.defence

    @property
    def get_attack_power(self) -> int:
        return self.attack_power


class Elf(Character):
    def __init__(self):
        super().__init__(name="Elf", health=50, defence=10, attack_power=15)
    
    def attack(self, target: Character):
        attack_power = super().get_attack_power
        if super().health_percentage < 0.3:
            attack_power *= 2
        super().attack_with_power(target, attack_power)
        

class Ork(Character):
    def __init__(self):
        super().__init__(name="Ork", health=100, defence=10, attack_power=10)
    
    @property
    def get_defence(self) -> int:
        return self.defence * 3 if self.health < 50 else self.defence

def do_battle(char_1: "Character", char_2: "Character"):
    print(f"Битва начинается")
    is_char_1_active = True
    while (char_1.health > 0 and char_2.health > 0):
        if is_char_1_active:
            char_1.attack(char_2)
        else:
            char_2.attack(char_1)
        is_char_1_active = not is_char_1_active
    print(f"Битва закончилась победой {char_2.name if is_char_1_active else char_1.name}")

char_1 = Ork()
char_2 = Elf()

do_battle(char_1, char_2)