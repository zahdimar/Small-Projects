/* LED controller. Button click switchs edited LED. Potentiometr value sets as LED brightness
*/

#define RED 5
#define GREEN 6
#define BLUE 9
#define POTENTIOMETR A5

#define BUTTON 4

int indexToColor[] = {RED, GREEN, BLUE};
int editLedNumber = 3; //No selected LED

boolean button_flag = 0;
boolean button = 0;
unsigned long last_press;

void setup() {
  pinMode(BUTTON, INPUT_PULLUP);
  pinMode(POTENTIOMETR, INPUT);
  pinMode(RED, OUTPUT);
  pinMode(GREEN, OUTPUT); 
  pinMode(BLUE, OUTPUT);
  Serial.begin(9600);
}

void loop() {
  int potentiometr = analogRead(A5);
  potentiometr = map(potentiometr,  0, 1023, 0, 255);
  potentiometr = constrain(potentiometr, 0, 255);
    
  button = digitalRead(BUTTON);
  if (button_flag == 0 && button == 1 && millis() - last_press > 100) {
    Serial.print("Button pressed. LED: ");
    button_flag = 1;
    editLedNumber = (editLedNumber + 1) % 4;
    Serial.println(editLedNumber);
    last_press = millis();
   }
   
   if (button ==0 && button_flag ==1) {
    button_flag = 0;
    Serial.println("Button released");
   }

   if (editLedNumber >= 0 && editLedNumber < 3) {
     analogWrite(indexToColor[editLedNumber], potentiometr);
   }
}
