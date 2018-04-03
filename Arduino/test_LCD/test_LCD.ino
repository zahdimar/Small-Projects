#include <Wire.h> 
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27, 16, 2);

void setup()
{
  lcd.begin();
  lcd.backlight();
}

int minutes = 1;
int seconds = 0;

void loop()
{
  if (seconds <= 0 ) {
    if (minutes == 0) {
      lcd.clear();
      lcd.print("!!BADABUM!!");
      delay(10000);
      minutes = 2;
      seconds = 0;
    }
    else {
      minutes--;
      seconds = 60;
    
    } 
  }
  seconds--;
  lcd.clear();
  lcd.print("Time left ");
  lcd.print(minutes);
  lcd.print(":");
  lcd.print(seconds);
  delay(980);
}
