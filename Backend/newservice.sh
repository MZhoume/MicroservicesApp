#!/bin/bash

if [ $# -ne 1 ]; then
    echo $0: usage: Service Name
    exit 1
fi

name=$1

# create service
pushd src
mkdir $name
cp ExampleFunction/aws-lambda-tools-defaults.json $name/
cp ExampleFunction/Function.cs $name/
cp ExampleFunction/project.json $name/

pushd $name
sed -i '' -e "s/ExampleFunction/$name/g" aws-lambda-tools-defaults.json
sed -i '' -e "s/ExampleFunction/$name/g" Function.cs

popd
popd

# create service test
pushd test
testname="$name.Test"
mkdir $testname
cp ExampleFunction.Test/FunctionTest.cs $testname/
cp ExampleFunction.Test/project.json $testname/

pushd $testname
sed -i '' -e "s/ExampleFunction/$name/g" FunctionTest.cs
sed -i '' -e "s/ExampleFunction/$name/g" project.json

popd
popd

echo 'DONE. Have fun ;)'
