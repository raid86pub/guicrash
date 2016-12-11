#!/bin/bash

export PATH=/bin:$PATH
WDIR=`dirname $0`
echo "" > $WDIR/crashcmd.log
cat $WDIR/crashcmd.log.copy > $WDIR/crashcmd.log
