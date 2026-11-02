\ Hardware definition for my board

\ From mecrisp's hexdump.fs:
\ : u.4 ( u -- ) 0 <# # # # # #> type ;
\ : u.2 ( u -- ) 0 <# # # #> type ;

\ : h.4 ( u -- ) base @ hex swap  u.4  base ! ;
\ : h.2 ( u -- ) base @ hex swap  u.2  base ! ;
compiletoflash

#include g6u-board.fs

\ Unattended operation, from jcw's embello:
compiletoflash

: rx-connected? ( -- f )  \ true if RX is connected (and idle)
  PA10 ioc!  IMODE-PULL PA10 io-mode!  PA10 io@ 0<>  OMODE-AF-PP PA10 io-mode!
  dup if 1 ms serial-key? if serial-key drop then then \ flush any input noise
;

: fake-key? ( -- f )  \ check for RX pin being pulled high
  rx-connected? if reset then false ;

\ unattended quits to the interpreter if the RX pin is connected, not floating
\ else it replaces the key? hook with a test to keep checking for RX reconnect
\ if so, it will reset to end up in the interpreter on the next startup
\ for use with a turnkey app in flash, i.e. ": init init unattended ... ;"

: unattended
  rx-connected? if quit then \ return to command prompt
  ['] fake-key? hook-key? ! ;

compiletoram
