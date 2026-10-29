\ Fan actually works inverted:
\ 100% is stop, 0% is full rpm

\ Compile to flash and run infinitely 
\ Assumes already loaded 'suf' with lots of hardware support 

\ <<<core>>>  https://jeelabs.wippler.nl/article/1709d/ jcw said to do this but it fails for me because it resets the uC )

compiletoflash

pc13 constant led
: led-init omode-pp led io-mode! ;
: led-on led ioc! ;
: led-off led ios! ;

pb0 constant fan
50 variable last%					    

: fan-init ( freq --- )
  fan pwm-init
  5000 fan pwm ;

: fan% ( u -- ) dup last% @ <> if
	 ." setting fan " cr
	 dup last% !
	 100 * fan pwm
       else
	 drop
       then ; 

pa2 constant potmeter

: pot-init ( freq -- )
  imode-adc potmeter io-mode! adc-init
  fan-init ;
: pot-trouble ( --- ) ( trouble TODO: ms crashes the uC )
  begin potmeter adc 40 /  fan% 100 ms key? until ;

: pot ( --- )
  begin potmeter adc 40 / dup . cr fan% 200 ms usb-key? until ;

: fan-off 100 fan% ;
: fan-max 0 fan% ;

( 1000 potmeter-init )
( pot )

\ Tried something with unattended, but not succesful, and why bother?

: init init 1000 pot-init  pot ;

compiletoram
