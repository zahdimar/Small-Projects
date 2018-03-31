boolean button_flag = 0;
boolean button = 0;
unsigned long last_press;

#define LEDS_COUNT 5

boolean leds[LEDS_COUNT] = {0, 0, 0, 0, 0};

void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(0, INPUT_PULLUP);
  pinMode(1, OUTPUT);
  pinMode(2, OUTPUT);
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  Serial.begin(9600);
}

// the loop function runs over and over again forever
void loop() {
   button = digitalRead(0);
   if (button_flag == 0 && button == 1 && millis() - last_press > 100) {
    button_flag = 1;
    Serial.println("button pressed");

    int led_to_switch_on = 0;
    Serial.println("1");
    for (led_to_switch_on = 0; leds[led_to_switch_on] != 0; led_to_switch_on ++) { 
      Serial.println("in loop");
      }
    
    if (led_to_switch_on < LEDS_COUNT) {
      leds[led_to_switch_on] = 1;
      Serial.print("Led to switch on is: ");
      Serial.println(led_to_switch_on);
    }
    
    last_press = millis();
   }
   
   if (button ==0 && button_flag ==1) {
    button_flag = 0;
    Serial.println("Button released");
   }

   for (int i = 0; i < LEDS_COUNT; i++) {
      digitalWrite(i+1, leds[i]);
    }
}
