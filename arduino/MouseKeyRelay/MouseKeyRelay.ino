/*
  KeyboardAndMouseControl

  Controls the mouse from five pushbuttons on an Arduino Leonardo, Micro or Due.

  Hardware:
  - five pushbuttons attached to D2, D3, D4, D5, D6

  The mouse movement is always relative. This sketch reads four pushbuttons, and
  uses them to set the movement of the mouse.

  WARNING: When you use the Mouse.move() command, the Arduino takes over your
  mouse! Make sure you have control before you use the mouse commands.

  created 15 Mar 2012
  modified 27 Mar 2012
  by Tom Igoe

  This example code is in the public domain.

  http://www.arduino.cc/en/Tutorial/KeyboardAndMouseControl
*/

#include "Keyboard.h"
#include "Mouse.h"
#include "SoftwareSerial.h"

SoftwareSerial mySerial(8, 3);

void setup() { // initialize the buttons' inputs:

  Serial.begin(9600);
  // initialize mouse control:
  Mouse.begin();
  Keyboard.begin();
  /*
  while (!Serial) {
    ; // シリアルポートの準備ができるのを待つ(Leonardoのみ必要)
  }
  */
  mySerial.begin(9600);
  Keyboard.releaseAll();
  Mouse.release(MOUSE_LEFT);
  Mouse.release(MOUSE_RIGHT);
  Mouse.release(MOUSE_MIDDLE);
}

void loop() {
  // use serial input to control the mouse:
  if(mySerial.available() > 0) {
    String inString = mySerial.readStringUntil(';');
    // mySerial.writeString(inChar);
    int inChar = inString.toInt();

    if (inChar == 0){
      Keyboard.releaseAll();
      Mouse.release(MOUSE_LEFT);
      Mouse.release(MOUSE_RIGHT);
      Mouse.release(MOUSE_MIDDLE);
    } else if (inChar < 0x200){
      // キーダウン
      Keyboard.press(inChar);
      return;
    } else if (inChar < 0x400){
      // キーアップ
      Keyboard.release(inChar - 0x200);
      return;          
    } else if (inChar < 0x460){
      // マウスボタン
      switch (inChar){
        case 0x400:
          Mouse.release(MOUSE_LEFT);
          return;
        case 0x401:
          Mouse.click(MOUSE_LEFT);
          return;
        case 0x402:
          Mouse.press(MOUSE_LEFT);
          return;
        case 0x420:
          Mouse.release(MOUSE_RIGHT);
          return;
        case 0x421:
          Mouse.click(MOUSE_RIGHT);
          return;
        case 0x422:
          Mouse.press(MOUSE_RIGHT);
          return;
        case 0x440:
          Mouse.release(MOUSE_MIDDLE);
          return;
        case 0x441:
          Mouse.click(MOUSE_MIDDLE);
          return;
        case 0x442:
          Mouse.press(MOUSE_MIDDLE);
          return;
        }
    } else if (inChar < 0x470) {
      // マウスホイール 上
      Mouse.move(0, 0, inChar - 0x460);
    } else if (inChar < 0x600){
      // マウスホイール 下
      Mouse.move(0, 0, -1 * (inChar - 0x470));
    } else if (inChar < 0x680) {
      Mouse.move(inChar - 0x600, 0);
    } else if (inChar < 0x700){
      Mouse.move(-1 * (inChar - 0x680), 0);
    } else if (inChar < 0x780){
      Mouse.move(0, inChar - 0x700);
    } else if (inChar < 0x800){
      Mouse.move(0, -1 * (inChar - 0x780));
    }
    delay(1);
  }
}
