import unittest
import math


def get_floor(flat_number):
    max_floors = 5
    flats_per_floor = 4

    enterance_flats_count = max_floors * flats_per_floor

    target_enterance_number = math.ceil(flat_number / enterance_flats_count)
    target_floor = flat_number % target_enterance_number % flats_per_floor + 1
    return (target_enterance_number, target_floor)


def print_floor(flat_number):
    info = get_floor(flat_number)
    
    print(f"Квартира {flat_number} находится в {info[0]} подъезде на {info[1]} этаже")



class TestStringMethods(unittest.TestCase):

    def test_upper(self):
        self.assertEqual('foo'.upper(), 'FOO')

    def test_isupper(self):
        self.assertTrue('FOO'.isupper())
        self.assertFalse('Foo'.isupper())

    def test_split(self):
        s = 'hello world'
        self.assertEqual(s.split(), ['hello', 'world'])
        # check that s.split fails when the separator is not a string
        with self.assertRaises(TypeError):
            s.split(2)

    def test_first_enterance_first_floor(self):
        result = get_floor(2)
        enterance = result[0]
        floor = result[1]
        self.assertEqual(enterance, 1)
        self.assertEqual(floor, 1)

if __name__ == '__main__':
    unittest.main()