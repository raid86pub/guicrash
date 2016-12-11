#!/bin/bash

export PATH=/bin:$PATH
if  [ "xx_" = "xx_$1" ]; then
	echo "ERROR 1" > /tmp/vb.result.out.txt
	exit 0
fi

WDIR=`dirname $0`
if [ ! -f $WDIR/crashcmd.log_$1 ]; then
	echo "ERROR 2" > /tmp/vb.result.out.txt
	exit 0
fi
if [ ! -f $WDIR/autocode.log_$1 ]; then
	echo "ERROR 3" > /tmp/vb.result.out.txt
	exit 0
fi

echo "" > $WDIR/crashcmd.log
echo "" > $WDIR/autocode.log
cat $WDIR/crashcmd.log_$1 > $WDIR/crashcmd.log
cat $WDIR/autocode.log_$1 > $WDIR/autocode.log
