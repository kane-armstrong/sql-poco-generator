pipeline {
    agent any
    stages {
        stage('Clean') {
            steps {
                sh 'dotnet clean'
            }
        }
        stage('Restore packages') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
    }
}