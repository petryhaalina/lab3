@echo off
setlocal enabledelayedexpansion


if "%~1"=="" (
    set /p directory=Enter directory path: 
) else (
    set "directory=%~1"
)

if not exist "%directory%" (
    echo Error: Directory "%directory%" not found.
    echo Program exited with code 1.
    pause
    exit /b 1
)

set /p maxFiles=Enter the maximum number of files to count: 

set "totalFiles=0"
for /r "%directory%" %%A in (*) do (
    attrib "%%A" | find /I "H" >nul
    if !errorlevel! equ 0 (
        set /a totalFiles+=1
    ) else (
        attrib "%%A" | find /I "A" >nul
        if !errorlevel! equ 0 (
            set /a totalFiles+=1
        ) else (
            attrib "%%A" | find /I "R" >nul
            if !errorlevel! equ 0 (
                set /a totalFiles+=1
            )
        )
    )
    
    if !totalFiles! geq !maxFiles! (
        echo Warning: Number of files exceeds the maximum limit of %maxFiles%.
        echo Program exited successfully with code 0.
        pause
        exit /b 0
    )
)

echo Number of files in subdirectories: %totalFiles%
echo Program exited successfully with code 0.

pause
exit /b 0
