import math
import unittest


def get_enterance_and_floor(flat_number, max_floors = 5, flats_per_floor = 4) -> tuple[int, int]:
    flats_per_enterance = max_floors * flats_per_floor

    enterance_number = math.ceil (flat_number / (max_floors * flats_per_floor))
    floor_number = math.ceil((flat_number % flats_per_enterance) / flats_per_floor)

    return enterance_number, floor_number


def print_floor(flat_number):
    info = get_enterance_and_floor(flat_number)
    
    print(f"Квартира {flat_number} находится в {info[0]} подъезде на {info[1]} этаже")


class TestStringMethods(unittest.TestCase):
    def check_floor(self, flat_number: int, expected_floor: int) -> list:
        _, floor = get_enterance_and_floor(flat_number)
        self.assertEqual(floor, expected_floor, "floor")

    def check_enterance(self, flat_number: int, expected_enterance: int) -> list:
        enterance, _ = get_enterance_and_floor(flat_number)
        self.assertEqual(enterance, expected_enterance, "enterance")

#  floors
    def test_first_floor(self):
        self.check_floor(flat_number=2, expected_floor=1)

        
    def test_second_floor(self):
        self.check_floor(flat_number=5, expected_floor=2)


    def test_fourth_floor(self):
        self.check_floor(
            flat_number=15,
            expected_floor=4)


    def test_fifth_floor(self):
        self.check_floor(flat_number=19, expected_floor=5)


    def test_fifth_floor(self):
        self.check_floor(flat_number=20, expected_floor=5)

        
    def test_fifth_floor(self):
        self.check_floor(flat_number=21, expected_floor=1)


    def test_fifth_floor(self):
        self.check_floor(flat_number=35, expected_floor=4)


    def test_fifth_floor(self):
        self.check_floor(flat_number=135, expected_floor=4)


# enterances
    def test_1st_enterance(self):
        self.check_enterance(2, 1)
    
    def test_1st_enterance(self):
        self.check_enterance(20, 1)

    def test_2nd_enterance(self):
        self.check_enterance(21, 2)

    def test_2nd_enterance(self):
        self.check_enterance(39, 2)

    def test_7th_enterance(self):
        self.check_enterance(139, 7)


if __name__ == '__main__':
    unittest.main()