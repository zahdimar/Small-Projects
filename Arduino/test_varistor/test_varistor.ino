unsigned long last_press;

#define LEDS_COUNT 5
#define LEDS_SHIFT 2
boolean leds[LEDS_COUNT];

int minValue = 0;
int maxValue = 1023;

void setup() {
  pinMode(A5, INPUT);
  for (int i =0; i < LEDS_COUNT; i++) {
      pinMode(i + LEDS_SHIFT, OUTPUT);
      leds[i] = 0;    
  }
  Serial.begin(9600);
}

void loop() {
    int currentValue = analogRead(A5);
    
    if (currentValue > maxValue)
        maxValue = currentValue;

    int levelWidth = maxValue / LEDS_COUNT;
    int currentLevel = currentValue / levelWidth;

     for (int i = 0; i < LEDS_COUNT; i++) {
       boolean switch_on = i < currentLevel;
       digitalWrite(i + LEDS_SHIFT, switch_on);
    }
}
