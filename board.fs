\ Hardware definition for my board

\ 4 constant io-ports

\ From mecrisp's hexdump.fs:
\ : u.4 ( u -- ) 0 <# # # # # #> type ;
\ : u.2 ( u -- ) 0 <# # # #> type ;

\ : h.4 ( u -- ) base @ hex swap  u.4  base ! ;
\ : h.2 ( u -- ) base @ hex swap  u.2  base ! ;

\ Includes from embello flib:

\ #include stm32f1/io.fs
\ #include pkg/pins48.fs
\  #include stm32f1/hal.fs  TODO cannot import this directly because it probably nukes some stuff that USB needs. Let's find one from embello/suf
\ #include suf/hal-stm32f1.fs
\ #include stm32f1/timer.fs
\ #include stm32f1/pwm.fs
\ #include stm32f1/adc.fs

#include g6u-board.fs

