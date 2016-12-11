#!/bin/bash

export PATH=/bin:$PATH
if  [ "xx_" = "xx_$1" ]; then
	echo
	#echo "ERROR" > /tmp/vb.result.out.txt
	#exit 0
fi

WDIR=`dirname $0`
echo "Cleared manually" > $WDIR/crashcmd.log
echo "Cleared manually" > $WDIR/autocode.log
