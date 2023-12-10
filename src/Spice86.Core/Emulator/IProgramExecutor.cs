namespace Spice86.Core.Emulator;

using Spice86.Core.Emulator.Debugger;
using Spice86.Core.Emulator.Pause;

public interface IProgramExecutor : IDisposable, IPauseable, IDebuggableComponent {
    void Run();
    void DumpEmulatorStateToDirectory(string path);
    bool IsGdbCommandHandlerAvailable { get; }
    void StepInstruction();
}