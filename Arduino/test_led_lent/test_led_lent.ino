#include <FastLED.h>
#define LED_PIN 12
#define NUM_LEDS    120
#define BRIGHTNESS  64
#define LED_TYPE    WS2811
#define COLOR_ORDER GRB

CRGB leds[NUM_LEDS];

#define UPDATES_PER_SECOND 100

// Этот скетч демонстрирует, как при помощи библиотеки FastLED
// создавать и использовать цветовые палитры.
//
// Пользоваться палитрами на практике – это гораздо проще,
// чем читать о них в теории. Поэтому просто запустите скетч
// и понаблюдайте за поведением светодиодов, попутно читая код.
// Этот скетч умеет создавать восемь (или больше) разных палитр,
// но его скомпилированная версия на AVR составляет всего 6,5 Кб.
//
// В библиотеку FastLED уже встроено несколько палитр. Кроме того,
// функционал библиотеки позволяет без особого труда создавать
// собственные палитры.
//
// Теорию того, как работают так называемые «компактные палитры»
// библиотеки FastLED, читайте в самом конце скетча.
 
CRGBPalette16 currentPalette;
TBlendType    currentBlending;
 
extern CRGBPalette16 myRedWhiteBluePalette;
extern const TProgmemPalette16 myRedWhiteBluePalette_p PROGMEM;
 
void setup() {
    delay( 3000 ); // небольшая задержка для того, чтобы цепь
                   // «устаканилась» после включения питания
 
    FastLED.addLeds<LED_TYPE, LED_PIN, COLOR_ORDER>(leds, NUM_LEDS).setCorrection( TypicalLEDStrip );
    FastLED.setBrightness(  BRIGHTNESS );
   
    currentPalette = RainbowColors_p;
    currentBlending = LINEARBLEND;
}
 
 
void loop()
{
    ChangePalettePeriodically();
   
    static uint8_t startIndex = 0;
    startIndex = startIndex + 1; // скорость движения
   
    FillLEDsFromPaletteColors( startIndex);
   
    FastLED.show();
    FastLED.delay(1000 / UPDATES_PER_SECOND);
}
 
void FillLEDsFromPaletteColors( uint8_t colorIndex)
{
    uint8_t brightness = 255;
   
    for( int i = 0; i < NUM_LEDS; i++) {
        leds[i] = ColorFromPalette( currentPalette, colorIndex, brightness, currentBlending);
        colorIndex += 3;
    }
}
 
// Этот фрагмент скетча демонстрирует несколько разных
// цветовых палитр. В библиотеку FasLED уже встроено несколько
// палитровых шаблонов: RainbowColors_p, RainbowStripeColors_p,
// OceanColors_p, CloudColors_p, LavaColors_p, ForestColors_p и
// PartyColors_p.
//
// Кроме того, вы можете создавать собственные палитры или даже
// написать код, создающий палитры прямо на ходу.
// Ниже продемонстрировано, как все это сделать.
 
void ChangePalettePeriodically()
{
    uint8_t secondHand = (millis() / 1000) % 60;
    static uint8_t lastSecond = 99;
   
    if( lastSecond != secondHand) {
        lastSecond = secondHand;
        if( secondHand ==  0)  { currentPalette = RainbowColors_p;         currentBlending = LINEARBLEND; }
        if( secondHand == 10)  { currentPalette = RainbowStripeColors_p;   currentBlending = NOBLEND;  }
        if( secondHand == 15)  { currentPalette = RainbowStripeColors_p;   currentBlending = LINEARBLEND; }
        if( secondHand == 20)  { SetupPurpleAndGreenPalette();             currentBlending = LINEARBLEND; }
        if( secondHand == 25)  { SetupTotallyRandomPalette();              currentBlending = LINEARBLEND; }
        if( secondHand == 30)  { SetupBlackAndWhiteStripedPalette();       currentBlending = NOBLEND; }
        if( secondHand == 35)  { SetupBlackAndWhiteStripedPalette();       currentBlending = LINEARBLEND; }
        if( secondHand == 40)  { currentPalette = CloudColors_p;           currentBlending = LINEARBLEND; }
        if( secondHand == 45)  { currentPalette = PartyColors_p;           currentBlending = LINEARBLEND; }
        if( secondHand == 50)  { currentPalette = myRedWhiteBluePalette_p; currentBlending = NOBLEND;  }
        if( secondHand == 55)  { currentPalette = myRedWhiteBluePalette_p; currentBlending = LINEARBLEND; }
    }
}
 
// Эта функция заполняет палитру совершенно случайными цветами.
 
void SetupTotallyRandomPalette()
{
    for( int i = 0; i < 16; i++) {
        currentPalette[i] = CHSV( random8(), 255, random8());
    }
}
 
// Эта функция делает палитру из черных и белых линий.
// Поскольку палитра – это, в сущности, массив
// из шестнадцати CRGB-цветов, для ее создания можно использовать
// различные функции fill_* – вроде fill_solid(), fill_gradient(),
// fill_rainbow() и т.д.
 
void SetupBlackAndWhiteStripedPalette()
{
    // сначала делаем все фрагменты черными...
    fill_solid( currentPalette, 16, CRGB::Black);
    // ...а потом делаем каждый четвертый фрагмент белым:
    currentPalette[0] = CRGB::White;
    currentPalette[4] = CRGB::White;
    currentPalette[8] = CRGB::White;
    currentPalette[12] = CRGB::White;
   
}
 
// Эта функция заполняет палитру фиолетовыми и зелеными полосами.
 
void SetupPurpleAndGreenPalette()
{
    CRGB purple = CHSV( HUE_PURPLE, 255, 255);
    CRGB green  = CHSV( HUE_GREEN, 255, 255);
    CRGB black  = CRGB::Black;
   
    currentPalette = CRGBPalette16(
                                   green,  green,  black,  black,
                                   purple, purple, black,  black,
                                   green,  green,  black,  black,
                                   purple, purple, black,  black );
}
 
// Фрагмент кода ниже показывает, как создать статичную палитру,
// хранящуюся в памяти PROGMEM (т.е. во flash-памяти).
// Этот тип памяти, как правило, просторней, чем RAM.
// Статичная палитра вроде той, создание которой показано ниже,
// занимает, как правило, 64 байта flash-памяти.
 
const TProgmemPalette16 myRedWhiteBluePalette_p PROGMEM =
{
    CRGB::Red,
    CRGB::Gray, // белый – слишком яркий свет
                // по сравнению с красным и синим
    CRGB::Blue,
    CRGB::Black,
   
    CRGB::Red,
    CRGB::Gray,
    CRGB::Blue,
    CRGB::Black,
   
    CRGB::Red,
    CRGB::Red,
    CRGB::Gray,
    CRGB::Gray,
    CRGB::Blue,
    CRGB::Blue,
    CRGB::Black,
    CRGB::Black
};
 
// Теперь немного теории о том, как работают компактные палитры
// библиотеки FastLED.
//
// В компьютерной графике, как правило, палитра
// (или «справочная таблица цветов») состоит из 256 фрагментов,
// которые содержат 256 разных 24-битных RGB-цветов. Соответственно,
// вы можете обратиться к нужному вам цвету при помощи простого
// 8-битного (т.е. 1-байтного) значения.
// Но 256-фрагментная палитра занимает 768 байт RAM-памяти,
// и для Arduino это, как правило, слишком много.
//
// FastLED поддерживает эти традиционные 256-фрагментные палитры -
// на тот случай, если RAM-память вашей сборки в состоянии потянуть
// необходимые 768 байт.
//
// Но в библиотеке FastLED есть и более компактная альтернатива.
// Эта функция называется «компактной палитрой» и состоит
// из 16 фрагментов. Впрочем, доступ к ней осуществляется так,
// будто на самом деле в ней 256 фрагментов, и это выполняется за счет
// интерполяции между имеющимися 16 фрагментами. Другими словами,
// между каждыми двумя смежными фрагментами генерируется пятнадцать
// виртуальных промежуточных фрагментов.
//
// К примеру, если для первого фрагмента компактной таблицы задать
// зеленый цвет (0,255,0), а для второго – синий (0,0,255),
// а затем запросить первые шестнадцать цветов, то библиотека вернет
// зеленый цвет, а следом – пятнадцать цветов, формирующих плавный
// градиент от зеленого к синему.
