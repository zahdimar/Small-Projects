#define SOUND_R A2         // аналоговый пин вход аудио, правый канал
#define SOUND_L A1         // аналоговый пин вход аудио, левый канал

int RcurrentLevel, LcurrentLevel;
int RsoundLevel, LsoundLevel;

void setup() {
  Serial.begin(9600);
  // put your setup code here, to run once:

}

void loop() {
    for (byte i = 0; i < 100; i ++) {                                 // делаем 100 измерений
          RcurrentLevel = analogRead(SOUND_R);                            // с правого
          LcurrentLevel = analogRead(SOUND_L);                 // и левого каналов
          if (RsoundLevel < RcurrentLevel) RsoundLevel = RcurrentLevel;   // ищем максимальное
          if (LsoundLevel < LcurrentLevel) LsoundLevel = LcurrentLevel;   // ищем максимальное
        }

  Serial.print(RsoundLevel);
  Serial.print("  ");
  Serial.println(LsoundLevel);
}
